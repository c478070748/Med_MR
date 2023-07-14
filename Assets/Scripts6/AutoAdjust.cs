using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAdjust : MonoBehaviour
{
    public GameObject center;

    public Vector3 offset;
    private void Start()
    {
        center = GameObject.FindGameObjectWithTag("Center");

        transform.position = center.transform.position + offset;
    }

   /* private IEnumerator adjust()
    {

        while ()
        {

        }

    }*/

}
