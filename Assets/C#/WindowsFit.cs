using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsFit : MonoBehaviour
{
    //Canvas¡–±Ì
    private CanvasScaler[] ca;

    void Awake()
    {
        ca = (CanvasScaler[])Resources.FindObjectsOfTypeAll<CanvasScaler>();
        foreach (var i in ca)
        {
            Debug.Log(i);
            i.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            i.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            i.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            i.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
        }
    }
}
