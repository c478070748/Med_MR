using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionManagment : MonoBehaviour
{
    public int[] questionList;
    private int curQuestionIndex = 0;
    public int errorRate;

    private int rightOptionIndex;

    public buttonTriggle[] options;

    public planeMove planeMove;

    public void triggleQestion(int index, int total)
    {
        if (curQuestionIndex > questionList.Length)
            return;

        if (questionList[curQuestionIndex] == index)
        {
            Global.status = Status.Read;

            sendOptions(index, total);

            curQuestionIndex++;
        }
    }


    public void sendOptions(int index, int total)
    {
        int left = Mathf.Max(index - errorRate, 0);
        int right = Mathf.Min(index + errorRate, total);
        int len = right - left;

        rightOptionIndex = Random.Range(0, 4);

        for(int i = 0; i < options.Length; i++)
        {
            options[i].id = i;
            options[i].testBk.color = Color.black;
            if (i == rightOptionIndex)
            {
                options[i].picture.texture = planeMove.textures[index];
            }
            else
            {
                int cur = Random.Range(0, total - len);
                cur = cur <= left ? cur : cur + len;

                options[i].picture.texture = planeMove.textures[cur];
            }
        }

        
    }

    public void clicked(int index)
    {
        Global.status = Status.Clicked;

        if (index != rightOptionIndex)
            options[index].testBk.color = Color.red;

        options[rightOptionIndex].testBk.color = Color.green;

        planeMove.updatePic();
    }


    public void next()
    {
        if (Status.Move != Global.status)
        {
            Global.status = Status.Move;

            planeMove.clearPic();
        }
    }

}
