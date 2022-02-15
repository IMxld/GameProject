using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBig : MonoBehaviour
{
    /// <summary>
    /// 鼠标进入按钮时触发
    /// </summary>
    public void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(1.1f, 1.1f);
    }

    /// <summary>
    /// 鼠标离开按钮时触发
    /// </summary>
    public void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1f, 1f);
    }
}
