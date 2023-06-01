using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public GameObject targetPanel;

    public GameObject center;

    public float distance = 10;

    public Vector3 rotateOffset;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindGameObjectWithTag("Center");

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = targetPanel.transform.rotation;
        transform.Rotate(rotateOffset);
        transform.position = center.transform.position + (-transform.forward * distance);
    }
}
