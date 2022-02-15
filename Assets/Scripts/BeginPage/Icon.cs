using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public Button[] buttons;
    public void ButtonDown(int _index)
    {
        GameObject _board = gameObject.transform.GetChild(_index).gameObject;
        if (_board.activeInHierarchy)
        {
            _board.SetActive(false);
            ChangeButtonCondition(true);
        }
        else
        {
            _board.SetActive(true);
            ChangeButtonCondition(false);
        }
    }
    public void ChangeButtonCondition(bool _bool)
    {
        foreach (Button _item in buttons)
        {
            _item.interactable = _bool;
        }
    }
}
