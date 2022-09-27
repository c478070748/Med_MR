using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonTriggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    public RawImage testBk;

    public RawImage picture;

    private questionManagment questionManagment;

    public int id;

    private void Start()
    {
        questionManagment = transform.parent.gameObject.GetComponent<questionManagment>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Global.status == Status.Read)
        {
            testBk.color = Color.gray;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Global.status == Status.Read)
        {
            testBk.color = Color.black;
        }
    }


    //没有“悬置”，可用OnPointerEnter+OnPointerExit模拟,例如：鼠标放在角色卡片，显示角色的生命值、攻击力、防御力


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Global.status == Status.Read)
        {
            questionManagment.clicked(id);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Global.status == Status.Read)
        {
            testBk.color = Color.white;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Global.status == Status.Read)
        {
            testBk.color = Color.black;
        }
    }

}
