using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActorPointDown : MonoBehaviour
{
    public Character character;
    delegate void Help(BaseEventData baseEventData);//ί�к���

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
    /// Ϊ�������eventtrigger�¼�
    /// </summary>
    /// <param name="eventTriggerType">�����¼�����</param>
    /// <param name="help">����Ҫִ�еĺ���</param>
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
