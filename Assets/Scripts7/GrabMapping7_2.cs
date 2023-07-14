using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GrabMapping7_2 : MonoBehaviour
{
    public Vector3 startPos, endPos;

    public int lastId;

    public bool isMapping = false;

    public GameObject renderPanel = null;

    public Texture2D[] textures;

    public string dirName;
    FileInfo[] files;
    int total;

    public float startY, endY;

    //public GameObject mainPanel;

    void Start()
    {
        string localPath = Application.streamingAssetsPath + @"\pics\" + dirName;
        //�ж��Ƿ����ĳ���ļ���
        if (Directory.Exists(localPath))
        {
            DirectoryInfo direction = new DirectoryInfo(localPath);
            files = direction.GetFiles("*.jpg");        //����ʲô���͵��ļ�
            //Debug.Log(files.Length);

            total = files.Length;
            textures = new Texture2D[total];

            //localPath + "/" + files[index].Name   : ���ڵõ��ļ���·��

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
        //����WWW
        WWW www = new WWW(url);

        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            //��ȡTexture
            textures[i] = www.texture;

            startTime = (double)Time.time - startTime;
            //Debug.Log("www������ʱ �� " + startTime);

        }


    }

    public void Init(float startY, float endY)
    {
        this.startY = startY;
        this.endY = endY;

    }

    public void UpdateStatus(int lastId)
    {
        this.lastId = lastId;
        isMapping = true;
    }

    void Update()
    {
        if (isMapping)
        {
            int curId = getTexId();
            if (curId != lastId)
            {
                //Debug.Log(curId);
                if (curId == -1)
                {
                    setTex(null);
                }
                else
                {
                    setTex(textures[curId]);
                }

                lastId = curId;

            }
        }

    }

    void setTex(Texture texture)
    {
        if (renderPanel != null)
        {
            renderPanel.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }
        else
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }
    }

    int getTexId()
    {
        float curY = transform.position.y;

        if (curY < endY || curY > startY)
        {
            return -1;
        }

        //Debug.Log("cur:" + Mathf.Abs(curY - startY) + "  end:" + Mathf.Abs(endY - startY));

        int res = (int)((Mathf.Abs(curY - startY) / Mathf.Abs(endY - startY)) * textures.Length);

        return res;
    }

    public void CloseMapping()
    {
        isMapping = false;
    }
}
