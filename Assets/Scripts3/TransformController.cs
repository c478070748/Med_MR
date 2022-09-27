using mattatz.TransformControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{

    // 1. Attatch TranformControl to target GameObject.
    public GameObject target;

    void Update()
    {
        // 2. Call TranformControl.Control() method in Update() loop.
        TransformControl tc = target.GetComponent<TransformControl>();
        tc.Control();
    }

}
