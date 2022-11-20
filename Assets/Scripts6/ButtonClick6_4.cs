using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick6_4 : MonoBehaviour
{
    public Color idle;
    public Color right;
    public Color click;

    public GameObject[] selections;

    public int CurSubmit;
    public void pressdown()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = click;
    }

    public void showAnwser(int index)
    {
        selections[index].GetComponent<MeshRenderer>().material.color = right;
    }

    public void clearColor()
    {
        foreach(GameObject selection in selections)
        {
            selection.GetComponent<MeshRenderer>().material.color = idle;
        }
    }

    public void A()
    {
        clearColor();
        selections[0].GetComponent<MeshRenderer>().material.color = click;

        CurSubmit = 0;
    }

    public void B()
    {
        clearColor();
        selections[1].GetComponent<MeshRenderer>().material.color = click;

        CurSubmit = 1;
    }

    public void C()
    {
        clearColor();
        selections[2].GetComponent<MeshRenderer>().material.color = click;

        CurSubmit = 2;
    }

    public void D()
    {
        clearColor();
        selections[3].GetComponent<MeshRenderer>().material.color = click;

        CurSubmit = 3;
    }
}
