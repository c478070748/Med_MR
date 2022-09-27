using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class verticalBar : MonoBehaviour
{
    public Scrollbar scrollbar;

    public double scale;

    public GameObject plane;

    private Vector3 start;

    private Vector3 direction;

    private void Start()
    {
        start = plane.transform.position;
        direction = plane.transform.up;
    }

    public void valueChanged()
    {
        float value = scrollbar.value;
        int offset = (int)(value * scale);
        plane.transform.position = start + direction * offset;
    }

    public void clickedUp()
    {
        double step = 1.0 / scale;
        scrollbar.value -= (float)step;
    }

    public void clickedDown()
    {
        double step = 1.0 / scale;
        scrollbar.value += (float)step;
    }
}
