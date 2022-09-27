using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class questionSystem : MonoBehaviour
{
    public int[] questionList;

    public static Texture2D[] textures;
    string localPath = @"E:\UnityProject\My project\Assets\pics\Series-001";
    FileInfo[] files;

    int total;
    int curIndex=0;

    public RawImage image;

    bool isLoaded = false;

    private Vector3 startPos;

    public GameObject mPlane;
    public GameObject target;

    void Start()
    {
        startPos = mPlane.transform.position;
        target.SetActive(false);

        //判断是否存在某个文件夹
        if (Directory.Exists(localPath))
        {
            DirectoryInfo direction = new DirectoryInfo(localPath);
            files = direction.GetFiles("*.jpg");        //加载什么类型的文件
            //Debug.Log(files.Length);

            total = files.Length;
            textures = new Texture2D[total];

            //localPath + "/" + files[index].Name   : 用于得到文件的路径

            for (int i = 0; i < files.Length; i++)
            {
                string url = localPath + "/" + files[i].Name;
                StartCoroutine(Load(url, i));
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
            //Debug.Log("www加载用时 ： " + startTime);

        }

        if (i == files.Length - 1)
        {
            isLoaded = true;
        }

    }

    private void Update()
    {
        if (isLoaded)
        {
            if (Global.status2 == Status2.SetQuestion)
            {
                if (curIndex < questionList.Length)
                {
                    //Debug.Log(curIndex);
                    setQuestion(curIndex);
                    Global.status2 = Status2.giveAnswer;
                }
            }
        }
        
    }

    void setQuestion(int index)
    {
        //Debug.Log(questionList[index]);
        image.texture = textures[questionList[index]];
    }

    public void commit()
    {
        if (Global.status2 == Status2.giveAnswer)
        {
            int offset = questionList[curIndex];

            target.transform.position += target.transform.up * offset;
            target.SetActive(true);

            Global.status2 = Status2.Check;
        }

        
    }

    public void next()
    {
        if (Global.status2 == Status2.Check)
        {
            target.transform.position = startPos;
            target.SetActive(false);

            curIndex++;
            Global.status2 = Status2.SetQuestion;
        }

    }
}
