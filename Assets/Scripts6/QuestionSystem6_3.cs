using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionSystem6_3 : MonoBehaviour
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

    public GameObject answer;

    public GameObject submit;

    EventTriggle6_3 eventTriggle;

    GameObject Center;
    void Start()
    {

        Init();

        

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

    void Init()
    {
        eventTriggle = GetComponent<EventTriggle6_3>();

        Center = GameObject.FindGameObjectWithTag("Center");
        
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");


        foreach (GameObject model in models)
        {
            answer.GetComponent<ClippingPlane>().AddRenderer(model.GetComponent<MeshRenderer>());
            submit.GetComponent<ClippingPlane>().AddRenderer(model.GetComponent<MeshRenderer>());
            
        }


        answer.SetActive(false);
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
        //调整面板贴图
        //Debug.Log(questionList[index]);
        plane.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[questionList[index]]);


        
        //调整答案位置
        Vector3 direction = -answer.transform.up;
        float offset = questionList[curIndex];
        if (curIndex != 0)
            offset = (questionList[curIndex] - questionList[preIndex]);

        float CenterScale = Center.transform.localScale.x;
        offset *= CenterScale;
        answer.transform.position += direction * offset;


        
    }

    public void commit()
    {
        if (Global.status2 == Status2.giveAnswer)
        {
            //去除提交的切割效果
            submit.GetComponent<ClippingPlane>().enabled = false;
            submit.GetComponent<ObjectManipulator>().enabled = false;

            //显示答案
            answer.GetComponent<ClippingPlane>().enabled = true;
            answer.SetActive(true);


            Global.status2 = Status2.Check;



            eventTriggle.OnCommit();
        }


    }

    public void start()
    {
        if (isLoaded == false)
        {
            isLoaded = true;

            answer.GetComponent<ClippingPlane>().enabled = false;
            answer.SetActive(false);

            submit.GetComponent<ClippingPlane>().enabled = true;
            submit.GetComponent<ObjectManipulator>().enabled = true;


            eventTriggle.OnStart();
            eventTriggle.OnNext();
        }
            
    }

    public void next()
    {


        if (Global.status2 == Status2.Check)
        {
            answer.GetComponent<ClippingPlane>().enabled = false;
            answer.SetActive(false);

            submit.GetComponent<ClippingPlane>().enabled = true;
            submit.GetComponent<ObjectManipulator>().enabled = true;

            curIndex++;
            Global.status2 = Status2.SetQuestion;

            eventTriggle.OnNext();
        }

    }

    public void gohome()
    {
        eventTriggle.OnExit();

        GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>().LoadOrUnloadScene("6_1");
    }
}
