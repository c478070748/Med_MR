using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTestComponents : MonoBehaviour
{
    public GameObject[] gameObjects;

    Interactable[] childs;

    private void Start()
    {
        childs = transform.GetComponentsInChildren<Interactable>();
    }

    // Start is called before the first frame update
    public void choose1()
    {
        switchVisualable(0, childs[0].IsToggled);
        Debug.Log(childs[0].IsToggled);
    }

    public void choose2()
    {
        switchVisualable(1, childs[1].IsToggled);
    }

    public void choose3()
    {
        switchVisualable(2, childs[2].IsToggled);
    }

    public void choose4()
    {
        switchVisualable(3, childs[3].IsToggled);
    }

    public void switchVisualable(int index,bool isOn)
    {
        gameObjects[index].SetActive(isOn);
    }
}
