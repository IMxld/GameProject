using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingDoor : MonoBehaviour
{
    public static RisingDoor _risingDoor;
    public static RisingDoor Instance
    {
        get
        {
            if (_risingDoor)
            {
                return _risingDoor;
            }
            else
            {
                _risingDoor = Transform.FindObjectOfType<RisingDoor>();
                return _risingDoor;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool CanIn = true;
            foreach (bool _item in other.gameObject.GetComponent<PlayerController>().IsKillBoss.Values)
            {
                if (!_item)
                {
                    CanIn = false;
                }
            }
            if (CanIn)
            {
                //触发通过
            }
            else
            {
                //玩家死亡
            }
        }
    }
}
