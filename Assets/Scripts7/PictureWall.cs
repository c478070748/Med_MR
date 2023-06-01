using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureWall : MonoBehaviour
{
    public GameObject wall;

    public GameObject ctPic, cutPic;

    public List<GameObject> ctPicList;

    public List<GameObject> cutPicList;

    public float spacing = 0.03f;

    public Camera renderCamera;

    float W, w;
    float H, h;
    float Sw,Sh, s;
    float c;//一个图片的边长

    private void Start()
    {
        Sw = wall.transform.parent.localScale.x;
        Sh = wall.transform.parent.localScale.y;
        s = ctPic.transform.parent.localScale.x;
        c = ctPic.GetComponent<MeshRenderer>().bounds.size.y;

        W = wall.GetComponent<MeshRenderer>().bounds.size.x;
        w = ctPic.GetComponent<MeshRenderer>().bounds.size.y * 2 + spacing * 3;

        H = wall.GetComponent<MeshRenderer>().bounds.size.y;
        h = ctPic.GetComponent<MeshRenderer>().bounds.size.y + spacing * 2;

        //W *= Sw;
        //H *= Sh;
        //w *= s;
        //h *= s;
        
        Debug.Log("W:" + W + " w:" + w + " H:" + H + " h:" + h + " Sw:" + Sw + " s:" + s);
    }

    //public Vector3 ScaleOffset = new Vector3(0.5f, 0.5f, 0.5f);

    public void SavePic()
    {
        int index = ctPicList.Count;
        int num_row = (int)(W / w);

        int row = index / num_row;
        int col = index % num_row;

        Vector3 archol = wall.transform.position + new Vector3(-W / 2, H / 2, 0);
        Vector3 pos = archol + new Vector3(col * w + w / 2, -(row * h + h / 2), 0);
        Vector3 ctPos = pos + new Vector3(-(w / 2 - spacing*2), 0, -0.01f);
        Vector3 cutPos = pos + new Vector3(w / 2 - spacing*2, 0, -0.01f);

        GameObject root = new GameObject(index + "_root");
        GameObject ctObj = GameObject.Instantiate(ctPic);
        GameObject cutObj = GameObject.Instantiate(cutPic);

        //ctObj.transform.localScale *= s;
        //cutObj.transform.localScale *= s;

        ctObj.name = index + "_ct";
        cutObj.name = index + "_cut";

        ctObj.transform.position = ctPos;
        cutObj.transform.position = cutPos;

        ctObj.transform.rotation = wall.transform.rotation;
        cutObj.transform.rotation = wall.transform.rotation;

        root.transform.position = pos;
        ctObj.transform.SetParent(root.transform);
        cutObj.transform.SetParent(root.transform);
        root.transform.localScale = new Vector3(s, s, s);
        root.transform.SetParent(wall.transform);

        ctPicList.Add(ctObj);
        cutPicList.Add(cutObj);


        ctObj.GetComponent<MeshRenderer>().material = new Material(ctObj.GetComponent<MeshRenderer>().material);
        cutObj.GetComponent<MeshRenderer>().material = new Material(cutObj.GetComponent<MeshRenderer>().material);

        RenderTexture rt = renderCamera.targetTexture;
        cutObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", getTexture2d(rt));


    }

    public Texture2D getTexture2d(RenderTexture renderT)
    {
        if (renderT == null)
            return null;

        int width = renderT.width;
        int height = renderT.height;
        Texture2D tex2d = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderT;
        tex2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex2d.Apply();

        //byte[] b = tex2d.EncodeToPNG();
        //Destroy(tex2d); 

        //File.WriteAllBytes(Application.dataPath + "1.jpg", b); 
        return tex2d;
    }


}
