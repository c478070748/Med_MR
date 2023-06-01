using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMapping : MonoBehaviour
{
    public float startY, endY;

    public int lastId;

    public bool isMapping = false;
    // Update is called once per frame

    public GameObject renderPanel = null;

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
                    setTex(QuestionSystem6_2.textures[curId]);
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

        int res = (int)((Mathf.Abs(curY - startY) / Mathf.Abs(endY - startY))*QuestionSystem6_2.textures.Length);

        return res;
    }

    public void CloseMapping()
    {
        isMapping = false;
    }
}
