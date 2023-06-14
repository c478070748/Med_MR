using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionSystem6_2 : MonoBehaviour
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

    public GameObject target;

    public GameObject Center;

    private EventTriggle6_2 eventTriggle;


    public Vector2 cutCenter;//截图中心

    public float cutLen;//截图边长

    public Camera lookDownCamera;//俯瞰摄像机

    Vector3 originPos;

    public bool isRotation = false;

    void Start()
    {
        Center = GameObject.FindGameObjectWithTag("Center");
        //Center.GetComponent<BoxCollider>().enabled = true;
        eventTriggle = GetComponent<EventTriggle6_2>();

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

        //剪裁CT图
        MeshFilter meshFilter = plane.GetComponent<MeshFilter>();
        Vector2[] uvs = new Vector2[meshFilter.mesh.vertices.Length];
        uvs[0] = new Vector2(cutCenter.x - cutLen / 2, cutCenter.y - cutLen / 2);
        uvs[1] = new Vector2(cutCenter.x + cutLen / 2, cutCenter.y - cutLen / 2);
        uvs[2] = new Vector2(cutCenter.x - cutLen / 2, cutCenter.y + cutLen / 2);
        uvs[3] = new Vector2(cutCenter.x + cutLen / 2, cutCenter.y + cutLen / 2);
        meshFilter.mesh.uv = uvs;


        plane.transform.localScale *= cutLen;
        //Vector3[] vertice = meshFilter.mesh.vertices;
        //Vector3 origin = vertice[0];
        //vertice[0] = origin + new Vector3(cutCenter.x - cutLen / 2, cutCenter.y - cutLen / 2, 0);
        //vertice[1] = origin + new Vector3(cutCenter.x + cutLen / 2, cutCenter.y - cutLen / 2, 0);
        //vertice[2] = origin + new Vector3(cutCenter.x - cutLen / 2, cutCenter.y + cutLen / 2, 0);
        //vertice[3] = origin + new Vector3(cutCenter.x + cutLen / 2, cutCenter.y + cutLen / 2, 0);
        //meshFilter.mesh.vertices = vertice;

        Vector2 modelOffset = (cutCenter - new Vector2(0.5f, 0.5f)) / cutLen;//模型偏移量

        target.transform.SetParent(plane.transform);

        target.transform.localPosition -= new Vector3(modelOffset.x, modelOffset.y, 0);

        target.transform.SetParent(transform);


        //Debug.Log(modelOffset);
        //plane.transform.localScale *= cutLen;
        //Debug.Log(meshFilter.mesh.vertices);
        //Debug.Log(vertice[0]);
        //Debug.Log(vertice[1]);
        //Debug.Log(vertice[2]);
        //Debug.Log(vertice[3]);

        //设置俯瞰摄像机参数
        float y = plane.transform.localScale.x + 60;
        //Debug.Log(y);
        lookDownCamera.transform.localPosition += new Vector3(0, y, 0);

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
        plane.GetComponent<GrabMapping>()?.CloseMapping();

        //Debug.Log(questionList[index]);
        plane.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[questionList[index]]);


        Vector3 direction = -plane.transform.forward;
        float offset = questionList[curIndex];
        //if (curIndex != 0)
        //    offset = (questionList[curIndex] - questionList[preIndex]);

        float CenterScale = Center.transform.localScale.x;
        offset *= CenterScale;

        lookDownCamera.transform.SetParent(plane.transform);

        plane.transform.position = originPos + direction * offset;

        lookDownCamera.transform.SetParent(transform.parent);

        if (isRotation)
        {
            target.transform.SetParent(plane.transform);

            float x = 0;
            float y = 0;
            float z = Random.Range(0, 360);
            plane.transform.Rotate(new Vector3(x, y, z));


            target.transform.SetParent(transform);
        }
        

        plane.GetComponent<GrabMapping>()?.UpdateStatus(questionList[curIndex]);
    }

    public void commit()
    {
        


        if (Global.status2 == Status2.giveAnswer)
        {

            target.SetActive(true);


            Global.status2 = Status2.Check;


            eventTriggle.OnCommit();
        }


    }

    public void start()
    {
        


        if (isLoaded == false)
        {
            isLoaded = true;

            originPos = plane.transform.position;


            Vector3 direction = -plane.transform.forward;
            float offset = textures.Length - 1;
            //if (curIndex != 0)
            //    offset = (questionList[curIndex] - questionList[preIndex]);

            float CenterScale = Center.transform.localScale.x;
            offset *= CenterScale;

            Vector3 endPos = originPos + direction * offset;

            plane.GetComponent<GrabMapping>()?.Init(originPos.y, endPos.y);


            eventTriggle.OnStart();
            eventTriggle.OnNext();
        }
            



    }

    public void next()
    {
        

        if (Global.status2 == Status2.Check)
        {

            target.SetActive(false);

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
