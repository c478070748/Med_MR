using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionSystem6_4 : MonoBehaviour
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

    public GameObject[] options;

    public int errorRate;

    int rightOptionIndex;

    public GameObject selections;

    EventTriggle6_4 eventTriggle;

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

        eventTriggle = GetComponent<EventTriggle6_4>();

        Center = GameObject.FindGameObjectWithTag("Center");

        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");


        foreach (GameObject model in models)
        {
            plane.GetComponent<ClippingPlane>().AddRenderer(model.GetComponent<MeshRenderer>());
            
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
        //plane.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[questionList[index]]);


        
        //调整面板位置
        Vector3 direction = -plane.transform.up;
        float offset = questionList[curIndex];
        if (curIndex != 0)
            offset = (questionList[curIndex] - questionList[preIndex]);

        float CenterScale = Center.transform.localScale.x;
        offset *= CenterScale;
        plane.transform.position += direction * offset;


        sendOptions(questionList[curIndex], total);
        
    }

    public void sendOptions(int index, int total)
    {
        int left = Mathf.Max(index - errorRate, 0);
        int right = Mathf.Min(index + errorRate, total);
        int len = right - left;

        rightOptionIndex = Random.Range(0, 4);

        for (int i = 0; i < options.Length; i++)
        {
            Renderer pic = options[i].transform.Find("pic").GetComponent<Renderer>();
            if (i == rightOptionIndex)
            {
                pic.material.SetTexture("_MainTex", textures[index]);
                
            }
            else
            {
                int cur = Random.Range(0, total - len);
                cur = cur <= left ? cur : cur + len;

                pic.material.SetTexture("_MainTex", textures[cur]);
            }
        }


    }

    public void commit()
    {
        if (Global.status2 == Status2.giveAnswer)
        {

            selections.GetComponent<ButtonClick6_4>().showAnwser(rightOptionIndex);


            Global.status2 = Status2.Check;

            int CurSubmit = selections.GetComponent<ButtonClick6_4>().CurSubmit;


            eventTriggle.OnCommit(CurSubmit, rightOptionIndex);
        }


    }

    public void start()
    {
        if (isLoaded == false)
        {
            isLoaded = true;



            eventTriggle.OnStart();
            eventTriggle.OnNext();
        }
            
    }

    public void next()
    {


        if (Global.status2 == Status2.Check)
        {
            selections.GetComponent<ButtonClick6_4>().clearColor();

          

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
