using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBig : MonoBehaviour
{
    /// <summary>
    /// �����밴ťʱ����
    /// </summary>
    public void OnMouseEnter()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Enter"));
        this.transform.localScale += new Vector3(0.1f, 0.1f);
    }

    /// <summary>
    /// ����뿪��ťʱ����
    /// </summary>
    public void OnMouseExit()
    {
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
    }

    public void BackVoice()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Back"));
    }
    public void ButtonVoice()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Button"));
    }
}
