using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreToBegin : MonoBehaviour
{
    /// <summary>
    /// ����仯��ͼƬ
    /// </summary>
    [SerializeField]
    public Sprite img1, img2;

    /// <summary>
    /// ���̵�����ȡ��ʼ����"BeginPage"
    /// </summary>
    public void OnMouseDown()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Back"));
        SceneManager.LoadScene("BeginPage");
    }

    /// <summary>
    /// �����밴ťʱ����������ͼƬ
    /// </summary>
    public void OnMouseEnter()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Enter"));
        this.transform.localScale += new Vector3(0.1f, 0.1f);
        this.GetComponent<Image>().sprite = img2;
    }

    /// <summary>
    /// ����뿪��ťʱ����������ͼƬ
    /// </summary>
    public void OnMouseExit()
    {
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
        this.GetComponent<Image>().sprite = img1;
    }
}
