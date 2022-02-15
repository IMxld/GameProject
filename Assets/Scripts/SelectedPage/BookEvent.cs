using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BookEvent : MonoBehaviour
{
    public int BookNum;
    private delegate void Help(BaseEventData baseEventData);
    // Start is called before the first frame update
    void Start()
    {
        Help Click;
        Help Enter;
        Help Exit;
        Click = LoadDrama;
        Enter = MouseEnter;
        Exit = MouseExit;
        EventTrigger(EventTriggerType.PointerDown, Click);
        EventTrigger(EventTriggerType.PointerEnter, Enter);
        EventTrigger(EventTriggerType.PointerExit, Exit);
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

    private void LoadDrama(BaseEventData baseEventData)
    {
        PlayingDrama.playingDrama.LoadDrama(BookNum);
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
