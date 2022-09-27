using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum Status
{
    Move,
    Read,
    Clicked
}

public enum Status2
{
    SetQuestion,
    giveAnswer,
    Check,
}

public class Global : MonoBehaviour
{


    public static Status status = Status.Move;

    public static Status2 status2 = Status2.SetQuestion;
}
