using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionSystem6_5 : MonoBehaviour
{
    public int[] questionList;

    public static Texture2D[] textures;

    string localPath = Application.streamingAssetsPath + @"\pics\Series-001";
    FileInfo[] files;

    int total;
    int curIndex = 0;
    int preIndex = 0;

    public GameObject plane;

    bool isLoaded = false;

    private Vector3 startPos;

    //public GameObject answer;

    public GameObject pic;

    public Draw6_5 draw;

    void Start()
    {

        Init();

        

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

    void Init()
    {
        
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");


        foreach (GameObject model in models)
        {
            plane.GetComponent<ClippingPlane>().AddRenderer(model.GetComponent<MeshRenderer>());
            
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
                    preIndex = curIndex;
                    Global.status2 = Status2.giveAnswer;
                }
            }
        }

    }

    void setQuestion(int index)
    {
        //���������ͼ
        //Debug.Log(questionList[index]);
        //plane.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[questionList[index]]);


        
        //�������λ��
        Vector3 direction = -plane.transform.up;
        float offset = questionList[curIndex];
        if (curIndex != 0)
            offset = (questionList[curIndex] - questionList[preIndex]);

        offset *= 0.01f;
        plane.transform.position += direction * offset;


        pic.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[questionList[curIndex]]);


        
        
    }


    public void commit()
    {
        if (Global.status2 == Status2.giveAnswer)
        {

            

            


            Global.status2 = Status2.Check;
        }


    }

    public void start()
    {
        if (isLoaded == false)
        {
            isLoaded = true;

            

            
        }
            
    }

    public void next()
    {


        if (Global.status2 == Status2.Check)
        {

            draw.ClearLines();
          

            curIndex++;
            Global.status2 = Status2.SetQuestion;
        }

    }

    public void gohome()
    {
        GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>().LoadOrUnloadScene("6_1");
    }
}
