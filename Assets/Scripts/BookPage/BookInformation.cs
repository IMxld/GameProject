using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BookInformation : MonoBehaviour
{

    /// <summary>
    /// 弹窗部件
    /// </summary>
    public GameObject information;
    private delegate void Help(BaseEventData baseEventData);
    private void Start()
    {
        Help Click;
        Help Enter;
        Help Exit;
        Click = ShowBookImfo;
        Enter = MouseEnter;
        Exit = MouseExit;
        EventTrigger(EventTriggerType.PointerDown, Click);
        EventTrigger(EventTriggerType.PointerEnter, Enter);
        EventTrigger(EventTriggerType.PointerExit, Exit);


    }

    /// <summary>
    /// 激活弹窗、设置图片、填充文字
    /// </summary>
    public void ShowBookImfo(BaseEventData baseEventData)
    {
        information.GetComponent<BookImfoBoard>().LoadDramaImfo(gameObject.GetComponent<MainDrama>());
        information.SetActive(true);
    }

    /// <summary>
    /// 为物体添加eventtrigger事件
    /// </summary>
    /// <param name="eventTriggerType">交互事件类型</param>
    /// <param name="help">所需要执行的函数</param>
    private void EventTrigger(EventTriggerType eventTriggerType, Help help)//
    {
        EventTrigger temp = gameObject.AddComponent<EventTrigger>();
        temp.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        UnityAction<BaseEventData> callBack = new UnityAction<BaseEventData>(help);
        entry.callback.AddListener(callBack);
        temp.triggers.Add(entry);
    }

    private void MouseEnter(BaseEventData baseEventData)
    {
        gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<MainDrama>().ImageBeSelected;
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f);
    }

    private void MouseExit(BaseEventData baseEventData)
    {
        gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<MainDrama>().ImagePreference;
        gameObject.transform.localScale = new Vector3(1f, 1f);
    }

}
 