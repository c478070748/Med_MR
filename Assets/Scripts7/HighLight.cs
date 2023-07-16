using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour,IMixedRealityFocusHandler
{
    public void OnFocusEnter(FocusEventData eventData)
    {
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.8f));
        ///GetComponent<Renderer>().material.renderQueue = 3001;
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.3f));
        //GetComponent<Renderer>().material.renderQueue = 3000;
    }

    
}
