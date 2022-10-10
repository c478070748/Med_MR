using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw6_5 : MonoBehaviour
{
    Stack<LineRenderer> lineStack = new Stack<LineRenderer>();


    LineRenderer line ;

    public Color LineColor;

    int num = 0;

    public Transform Lines;

    public float width;

    public float offset;


    Vector3 originPos;
    Quaternion originRot;

    public Material lineMaterial;

    private void Start()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if(Global.status2 == Status2.giveAnswer)
        {
            if (LineColor == null)
            {
                return;
            }
            GameObject obj = new GameObject();
            obj.transform.SetParent(Lines);
            line = obj.AddComponent<LineRenderer>();
            lineStack.Push(line);
            line.material = lineMaterial;
            line.material.color = LineColor;
            line.material.SetColor("_EmissionColor", LineColor);
            line.widthMultiplier = width;

            Vector3 pos = eventData.Pointer.Result.Details.Normal * offset + eventData.Pointer.Result.Details.Point;

            line.SetPosition(0, pos);
            line.SetPosition(1, pos);
            num = 0;
        }
        
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //line.loop = true;
        transform.SetPositionAndRotation(originPos, originRot);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (Global.status2 == Status2.giveAnswer)
        {
            if (eventData.Pointer.Result.CurrentPointerTarget != null)
            {
                num++;
                line.positionCount = num;

                Vector3 pos = eventData.Pointer.Result.Details.Normal * offset + eventData.Pointer.Result.Details.Point;

                line.SetPosition(num - 1, pos);
            }

        }

        
        
    }

    public void ClearLines()
    {
        while (lineStack.Count > 0)
        {
            GameObject gameObject = lineStack.Pop().gameObject;
            Destroy(gameObject);
        }
    }


}
