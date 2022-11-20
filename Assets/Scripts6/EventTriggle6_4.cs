using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggle6_4 : MonoBehaviour
{
    List<KeyValuePair<string, string>> TestAtrrs = new List<KeyValuePair<string, string>>();

    List<List<KeyValuePair<string, string>>> ItemLists = new List<List<KeyValuePair<string, string>>>();

    public void OnStart()
    {
        string type, date, time;

        type = "test3";

        DateTime NowSystemTime = DateTime.Now.ToLocalTime();

        date = NowSystemTime.Year + "_" + NowSystemTime.Month + "_" + NowSystemTime.Day;

        time = NowSystemTime.Hour + "_" + NowSystemTime.Minute + "_" + NowSystemTime.Second;

        TestAtrrs.Add(new KeyValuePair<string, string>("type", type));
        TestAtrrs.Add(new KeyValuePair<string, string>("date", date));
        TestAtrrs.Add(new KeyValuePair<string, string>("time", time));

        
    }

    float QestionBegin, QestionEnd;

    public void OnNext()
    {
        QestionBegin = Time.time;
    }

    public void OnCommit(int submit , int answer)
    {
        QestionEnd = Time.time;

        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

        string StartTime, EndTime, SpendTime, Submit, Answer, IsRight;

        StartTime = QestionBegin.ToString();
        EndTime = QestionEnd.ToString();
        SpendTime = (QestionEnd - QestionBegin).ToString();
        Submit = submit.ToString();
        Answer = answer.ToString();
        IsRight = ((submit == answer) ? 1 : 0).ToString();
        

        keyValuePairs.Add(new KeyValuePair<string, string>("StartTime", StartTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("EndTime", EndTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("SpendTime", SpendTime));
        keyValuePairs.Add(new KeyValuePair<string, string>("Submit", Submit));
        keyValuePairs.Add(new KeyValuePair<string, string>("Answer", Answer));
        keyValuePairs.Add(new KeyValuePair<string, string>("IsRight", IsRight));


        ItemLists.Add(keyValuePairs);
    }

    public void OnExit()
    {
        XmlManager.AddTest(Login.CurFileName, TestAtrrs, ItemLists);
    }

}
