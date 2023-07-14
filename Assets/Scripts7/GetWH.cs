using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 length = GetComponent<MeshFilter>().mesh.bounds.size;
        float xlength = length.x * transform.lossyScale.x;
        float ylength = length.y * transform.lossyScale.y;
        float zlength = length.z * transform.lossyScale.z;

        Debug.Log("x:" + xlength);
        Debug.Log("y:" + ylength);
        Debug.Log("z:" + zlength);
    }

    
}
