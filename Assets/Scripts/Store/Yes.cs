using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yes : MonoBehaviour
{
    /// <summary>
    /// 所挂物体的父物体
    /// </summary>
    [SerializeField]
    public Image parent;

    /// <summary>
    /// 将将要变灰的物体启用并将父物体禁用
    /// </summary>
    public void OnMouseDown()
    {
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
        parent.gameObject.SetActive(false);
    }
}
