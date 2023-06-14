using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GrabMappingPlus : MonoBehaviour
{
    public Vector3 startPos, endPos;

    public int lastId;

    public bool isMapping = false;

    public GameObject renderPanel = null;

    public Texture2D[] textures;

    public string dirName;
    FileInfo[] files;
    int total;

    public enum dir
    {
        forward,
        back,
        up,
        down,
        left,
        right,
        none
    }

    public dir _dir = dir.none;

    public GameObject mainPanel;

    void Start()
    {
        string localPath = Application.streamingAssetsPath + @"\pics\" + dirName;
        //�ж��Ƿ����ĳ���ļ���
        if (Directory.Exists(localPath))
        {
            DirectoryInfo direction = new DirectoryInfo(localPath);
            files = direction.GetFiles("*.png");        //����ʲô���͵��ļ�
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

    Vector3 getDir(dir d)
    {
        switch (d)
        {
            case dir.forward:return Vector3.forward;
            case dir.back:return Vector3.back;
            case dir.up:return Vector3.up;
            case dir.down:return Vector3.down;
            case dir.right:return Vector3.right;
            case dir.left:return Vector3.left;
            default:return Vector3.zero;
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

        if (_dir != dir.none && mainPanel != null)
        {
            float dis = mainPanel.GetComponent<MeshRenderer>().bounds.size.x;
            Init(getDir(_dir), dis);

            UpdateStatus(-1);
        }

    }


    public void Init(Vector3 dir,float dis)
    {
        startPos = transform.position;
        endPos = transform.position + dir.normalized * dis;
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
        Vector3 CS = startPos - transform.position;
        Vector3 CE = endPos - transform.position;
        Vector3 SE = endPos - startPos;

        if (Vector3.Dot(CS, CE) < 0)
            return -1;

        float curY = transform.position.y;

        

        //Debug.Log("cur:" + Mathf.Abs(curY - startY) + "  end:" + Mathf.Abs(endY - startY));

        int res = (int)((Mathf.Abs(CS.magnitude) / Mathf.Abs(SE.magnitude)) * textures.Length);

        return res;
    }

    public void CloseMapping()
    {
        isMapping = false;
    }
}
