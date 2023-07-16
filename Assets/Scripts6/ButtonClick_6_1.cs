using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick_6_1 : MonoBehaviour
{
    SceneLoadManager sceneLoadManager;

    public GameObject ClippingPlane, ClippingBox, ClippingSphere;

    //public static Vector3 originPos = Vector3.zero;
    //public static Quaternion originRot = Quaternion.identity;

    private void Start()
    {
        Init();
        sceneLoadManager = GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>();
    }

    void Init()
    {
        Global.status2 = Status2.SetQuestion;
        GameObject[] models = GameObject.FindGameObjectsWithTag("Model");

        //GameObject Center = GameObject.FindGameObjectWithTag("Center");

        //if(originPos == Vector3.zero)
        //{
        //    originPos = Center.transform.position;
        //}

        //if (originRot == Quaternion.identity)
        //{
        //    originRot = Center.transform.rotation;
        //}

        //Center.transform.SetPositionAndRotation(originPos, originRot);
        //Center.GetComponent<BoxCollider>().enabled = false;

        foreach (GameObject model in models)
        {
            ClippingPlane.GetComponent<ClippingPlane>().AddRenderer(model.GetComponent<MeshRenderer>());
            ClippingBox.GetComponent<ClippingBox>().AddRenderer(model.GetComponent<MeshRenderer>());
            ClippingSphere.GetComponent<ClippingSphere>().AddRenderer(model.GetComponent<MeshRenderer>());
        }


    }

    public void button1()
    {
        sceneLoadManager.LoadOrUnloadScene("6_2");
    }

    public void button2()
    {
        sceneLoadManager.LoadOrUnloadScene("6_3");
    }
    public void button3()
    {
        sceneLoadManager.LoadOrUnloadScene("6_4");
    }
    public void button4()
    {
        sceneLoadManager.LoadOrUnloadScene("6_5");
    }

    public void button5()
    {
        sceneLoadManager.LoadOrUnloadScene("7_1");
    }

    public void button6()
    {
        sceneLoadManager.LoadOrUnloadScene("7_2");
    }

    public void button7()
    {
        sceneLoadManager.LoadOrUnloadScene("7_3");
    }
}
