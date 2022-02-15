using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActorPointDown : MonoBehaviour
{
    public Character character;
    delegate void Help(BaseEventData baseEventData);//委托函数

    // Start is called before the first frame update
    void Start()
    {
        Help help;
        help = ShowActorImfo;
        EventTrigger(EventTriggerType.PointerUp,help);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void ShowActorImfo(BaseEventData baseEventData)
    {
        LoadActors.loadActors.LoadImfo(character);
    }
    /// <summary>
    /// 为物体添加eventtrigger事件
    /// </summary>
    /// <param name="eventTriggerType">交互事件类型</param>
    /// <param name="help">所需要执行的函数</param>
    void EventTrigger(EventTriggerType eventTriggerType,Help help)//
    {
        EventTrigger temp = gameObject.AddComponent<EventTrigger>();
        temp.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        UnityAction<BaseEventData> callBack = new UnityAction<BaseEventData>(help);
        entry.callback.AddListener(callBack);
        temp.triggers.Add(entry);
    }

}
