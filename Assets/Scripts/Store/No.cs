using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class No : MonoBehaviour
{
    /// <summary>
    /// ��������ĸ�����
    /// </summary>
    [SerializeField]
    public Image parent;

    /// <summary>
    /// �����������
    /// </summary>
    public void OnMouseDown()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Button"));
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
        parent.gameObject.SetActive(false);
    }
}
