using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public ProgressIndicatorLoadingBar progressIndicatorLoadingBar;

    //public GameObject[] Models;
    private void Start()
    {

        Center = GameObject.FindGameObjectWithTag("Center");
        centerPosition = Center.transform.position;
        centerRotation = Center.transform.rotation;
        centerScale = Center.transform.localScale;
        //Models = GameObject.FindGameObjectsWithTag("Model");

        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sceneSystem.OnSceneLoaded += OnSceneLoaded;
        sceneSystem.OnWillUnloadScene += OnWillUnloadScene;

        LoadOrUnloadScene("6_1");
    }

    private void Update()
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();

        if (sceneSystem.SceneOperationInProgress)
        {
            DisplayProgressIndicator(sceneSystem.SceneOperationProgress);
        }
        else
        {
            HideProgressIndicator();
        }
    }

    private void DisplayProgressIndicator(float sceneOperationProgress)
    {
        progressIndicatorLoadingBar.gameObject.SetActive(true);
        progressIndicatorLoadingBar.Progress = sceneOperationProgress;
    }

    void HideProgressIndicator()
    {
        progressIndicatorLoadingBar.gameObject.SetActive(false);
    }

    public async void LoadOrUnloadScene(string SceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        await sceneSystem.LoadContent(SceneName, LoadSceneMode.Single);
    }


    Vector3 rightPosition = Vector3.zero;
    Quaternion rightRotation = Quaternion.identity;
    Vector3 rightScale = Vector3.zero;

    Vector3 leftPosition = Vector3.zero;
    Quaternion leftRotation = Quaternion.identity;
    Vector3 leftScale = Vector3.zero;

    GameObject Center;


    Vector3 centerPosition = Vector3.zero;
    Vector3 centerScale = Vector3.zero;
    Quaternion centerRotation = Quaternion.identity;

    private void OnSceneLoaded(string SceneName)
    {
        //Debug.Log(rightTransform);
        if (rightScale != Vector3.zero)
        {
            GameObject right = GameObject.FindGameObjectWithTag("Right");
            right.transform.position = rightPosition;
            right.transform.rotation = rightRotation;
            right.transform.localScale = rightScale;
        }

        /*if(leftPosition != Vector3.zero)
        {
            GameObject left = GameObject.FindGameObjectWithTag("Left");
            left.transform.position = leftPosition;
            left.transform.rotation = leftRotation;
            left.transform.localScale = leftScale;
        }*/

        if(SceneName == "6_1")
        {
            Center.GetComponent<BoxCollider>().enabled = true;
            Center.GetComponent<RotationAxisConstraint>().enabled = true;
            
        }else if(SceneName == "6_2" || SceneName == "7_1" || SceneName == "7_3")
        {
            Center.GetComponent<BoxCollider>().enabled = true;
            Center.GetComponent<RotationAxisConstraint>().enabled = false;
        }
        else
        {
            Center.GetComponent<BoxCollider>().enabled = false;
            
        }

        if (centerScale != Vector3.zero)
        {
            Center.transform.position = centerPosition;
            Center.transform.rotation = centerRotation;
            Center.transform.localScale = centerScale;


            GameObject Content = GameObject.FindGameObjectWithTag("Content");
            if (Content != null && SceneName != "7_3")
            {
                Content.transform.position = centerPosition;
                Content.transform.rotation = centerRotation;
                Content.transform.localScale = centerScale;
            }


            if (Center != null)
            {
                Center.transform.position = centerPosition;
                Center.transform.rotation = centerRotation;
                Center.transform.localScale = centerScale;
            }
            
            if(SceneName == "7_3")
            {
                Center.transform.localScale *= 0.5f;
            }
            //Transform root = Content.transform.parent;
            //if (root != null)
            //{
            //    root.position = centerPosition;
            //}

        }
    }

    private void OnWillUnloadScene(string SceneName)
    {
        GameObject right = GameObject.FindGameObjectWithTag("Right");
        if(right != null)
        {
            rightPosition = right.transform.position;
            rightRotation = right.transform.rotation;
            rightScale = right.transform.localScale;
        }

        /*GameObject left = GameObject.FindGameObjectWithTag("Left");
        if(left != null)
        {
            leftPosition = left.transform.position;
            leftRotation = left.transform.rotation;
            leftScale = left.transform.localScale;
        }*/

        //if (Center != null && SceneName == "6_1")
        //{
        //    centerPosition = Center.transform.position;
        //    centerRotation = Center.transform.rotation;
        //    centerScale = Center.transform.localScale;
        //}
    }

    //private void HandleSceneOperation(string SceneName)
    //{
    //    if(SceneName == "6_1")
    //    {
    //        GameObject[] ClipTools = GameObject.FindGameObjectsWithTag("ClipTool");


    //        foreach(GameObject clipTool in ClipTools)
    //        {


    //            if(clipTool.name == "ClippingSphere")
    //            {
    //                ClippingSphere clippingSphere = clipTool.GetComponent<ClippingSphere>();
    //                foreach(GameObject Model in Models)
    //                {
    //                    clippingSphere.AddRenderer(Model.GetComponent<MeshRenderer>());
    //                }
    //            }
    //            else if(clipTool.name == "ClippingBox")
    //            {
    //                ClippingBox clippingBox = clipTool.GetComponent<ClippingBox>();
    //                foreach (GameObject Model in Models)
    //                {
    //                    clippingBox.AddRenderer(Model.GetComponent<MeshRenderer>());
    //                }
    //            }
    //            else if (clipTool.name == "ClippingPlane")
    //            {
    //                ClippingPlane clippingPlane = clipTool.GetComponent<ClippingPlane>();
    //                foreach (GameObject Model in Models)
    //                {
    //                    clippingPlane.AddRenderer(Model.GetComponent<MeshRenderer>());
    //                }
    //            }
    //        }
    //    }else if (SceneName == "6_3")
    //    {
    //        GameObject[] ClipTools = GameObject.FindGameObjectsWithTag("ClipTool");


    //        foreach (GameObject clipTool in ClipTools)
    //        {

    //            if (clipTool.name == "ClippingPlane")
    //            {
    //                ClippingPlane clippingPlane = clipTool.GetComponent<ClippingPlane>();
    //                foreach (GameObject Model in Models)
    //                {
    //                    clippingPlane.AddRenderer(Model.GetComponent<MeshRenderer>());
    //                }
    //            }
    //        }
    //    }
    //}
}
