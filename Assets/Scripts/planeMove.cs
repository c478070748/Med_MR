using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class planeMove : MonoBehaviour
{

    public float distance;
    public float speed;
    public Vector3 dir;
    private Vector3 startPos;
    private int index;
    /// <summary>
    /// 图片总数
    /// </summary>
    private int total;

    private int curIndex;

    public static Texture2D[] textures;
    string localPath = @"E:\UnityProject\My project\Assets\pics\Series-001";
    FileInfo[] files;

    public questionManagment questionManagment;

    void Start()
    {
        startPos = transform.position;
        

        //判断是否存在某个文件夹
        if (Directory.Exists(localPath))
        {
            DirectoryInfo direction = new DirectoryInfo(localPath);
            files = direction.GetFiles("*.jpg");        //加载什么类型的文件
            //Debug.Log(files.Length);

            total = files.Length;
            textures = new Texture2D[total];

            //localPath + "/" + files[index].Name   : 用于得到文件的路径

            for(int i = 0; i < files.Length; i++)
            {
                string url = localPath + "/" + files[i].Name;
                StartCoroutine(Load(url,i));
            }
            
        }
    }

    IEnumerator Load(string url, int i)
    {
        double startTime = (double)Time.time;
        //请求WWW
        WWW www = new WWW(url);

        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            //获取Texture
            textures[i] = www.texture;

            startTime = (double)Time.time - startTime;
            Debug.Log("www加载用时 ： " + startTime);

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Global.status == Status.Move)
        {
            float dist = (transform.position - startPos).magnitude;
            if (dist < distance)
            {
                transform.position += dir * speed * Time.deltaTime;
                int old = index;
                index = (int)(dist / distance * total);

                if (old != index)
                {
                    //updatePic(index);
                    curIndex = index;
                    questionManagment.triggleQestion(index, total);
                }
            }
        }
        
    }

    public void updatePic()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_MainTex", textures[curIndex]);
    }

    public void clearPic()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_MainTex", default);
    }


}
