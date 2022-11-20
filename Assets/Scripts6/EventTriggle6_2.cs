using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggle6_2 : MonoBehaviour
{
    List<KeyValuePair<string, string>> TestAtrrs = new List<KeyValuePair<string, string>>();

    List<List<KeyValuePair<string, string>>> ItemLists = new List<List<KeyValuePair<string, string>>>();

    public void OnStart()
    {
        string type, date, time;

        type = "test1";

        DateTime NowSystemTime = DateTime.Now.ToLocalTime();

        date = NowSystemTime.Year + "_" + NowSystemTime.Month + "_" + NowSystemTime.Day;

        time = NowSystemTime.Hour + "_" + NowSystemTime.Minute + "_" + NowSystemTime.Second;

        TestAtrrs.Add(new KeyValuePair<string, string>("type", type));
        TestAtrrs.Add(new KeyValuePair<string, string>("date", date));
        TestAtrrs.Add(new KeyValuePair<string, string>("time", time));

        answer = GameObject.FindGameObjectWithTag("Center").transform;
    }

    float QestionBegin, QestionEnd;

    public void OnNext()
    {
        QestionBegin = Time.time;
    }

    public Transform answer, target;
    public void OnCommit()
    {
        QestionEnd = Time.time;

        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

        string StartTime, EndTime, SpendTime, PosDis, RotAngle;

        StartTime = QestionBegin.ToString();
        EndTime = QestionEnd.ToString();
        SpendTime = (QestionEnd - QestionBegin).ToString();
        PosDis = (Vector3.Distance(answer.position, target.position)).ToString();
        RotAngle = (Quaternion.Angle(answer.rotation, target.rotation)).ToString();

        keyValuePairs.Add(new KeyValuePair<string, string>("StartTime", StartTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("EndTime", EndTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("SpendTime", SpendTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("PosDis", PosDis));
        keyValuePairs.Add(new KeyValuePair<string, string>("RotAngle", RotAngle));

        ItemLists.Add(keyValuePairs);
    }

    public void OnExit()
    {
        XmlManager.AddTest(Login.CurFileName, TestAtrrs, ItemLists);
    }

}
