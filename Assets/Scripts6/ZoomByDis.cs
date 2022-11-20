using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomByDis : MonoBehaviour
{
    public bool isZoom = false;

    public float zoomFactory;

    float preDistance;


    public float minBound, maxBound, ratio;

    private void Start()
    {
        preDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        ratio = transform.localScale.y / transform.localScale.x;
    }

    void Update()
    {
        if (isZoom)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            float value = (distance - preDistance) * zoomFactory;
            Vector3 zoom = transform.localScale + new Vector3(value, ratio * value, 0);

            if (zoom.x < minBound)
            {
                zoom = new Vector3(minBound, minBound * ratio, zoom.z);
            }else if(zoom.x > maxBound)
            {
                zoom = new Vector3(maxBound, maxBound * ratio, zoom.z);
            }

            transform.localScale = zoom;

            preDistance = distance;
        }
    }

    public void OpenZoom()
    {
        isZoom = true;
    }

    public void CloseZoom()
    {
        isZoom = false;
    }
}
