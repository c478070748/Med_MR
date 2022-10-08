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
        //Models = GameObject.FindGameObjectsWithTag("Model");

        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        //sceneSystem.OnSceneLoaded += HandleSceneOperation;

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
