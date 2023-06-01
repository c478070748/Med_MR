using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudySystem7_2 : MonoBehaviour
{
    public GameObject targetPanel;

    public GameObject center;

    public Transform yAxisPos;

    void Start()
    {
        center = GameObject.FindGameObjectWithTag("Center");

        foreach (MeshRenderer renderer in center.GetComponentsInChildren<MeshRenderer>())
        {
            targetPanel.GetComponent<ClippingPlane>().AddRenderer(renderer);
        }
    }

    public enum CutStatus
    {
        Free,
        YAxis
    }

    public CutStatus cutStatus = CutStatus.Free;
    

    public void FreeCutBtn()
    {
        if (cutStatus == CutStatus.Free)
            return;

        cutStatus = CutStatus.Free;
        targetPanel.GetComponent<GrabMapping>()?.CloseMapping();

        targetPanel.GetComponent<RotationAxisConstraint>().ConstraintOnRotation = 0;
        targetPanel.GetComponent<MoveAxisConstraint>().ConstraintOnMovement = 0;
    }

    public void YAxisCutBtn()
    {
        if (cutStatus == CutStatus.YAxis)
            return;

        cutStatus = CutStatus.YAxis;

        SetYAxisStatus();
    }

    public void SetYAxisStatus()
    {
        //targetPanel.transform.position = yAxisPos.position;
        targetPanel.transform.position = new Vector3(center.transform.position.x, yAxisPos.position.y, center.transform.transform.position.z);
        targetPanel.transform.rotation = Quaternion.identity;

        targetPanel.GetComponent<RotationAxisConstraint>().ConstraintOnRotation = AxisFlags.XAxis | AxisFlags.YAxis | AxisFlags.ZAxis;
        targetPanel.GetComponent<MoveAxisConstraint>().ConstraintOnMovement = AxisFlags.XAxis | AxisFlags.ZAxis;


        Vector3 originPos = targetPanel.transform.position;

        Vector3 direction = -targetPanel.transform.up;
        float offset = QuestionSystem6_2.textures.Length - 1;
        //if (curIndex != 0)
        //    offset = (questionList[curIndex] - questionList[preIndex]);

        float CenterScale = center.transform.localScale.x;
        offset *= CenterScale;

        Vector3 endPos = originPos + direction * offset;

        targetPanel.GetComponent<GrabMapping>()?.Init(originPos.y, endPos.y);
        targetPanel.GetComponent<GrabMapping>()?.UpdateStatus(0);
    }


}
