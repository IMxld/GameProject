using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yes : MonoBehaviour
{
    /// <summary>
    /// ��������ĸ�����
    /// </summary>
    [SerializeField]
    public Image parent;

    /// <summary>
    /// ����Ҫ��ҵ��������ò������������
    /// </summary>
    public void OnMouseDown()
    {
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
        parent.gameObject.SetActive(false);
    }
}
