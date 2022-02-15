using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : MonoBehaviour
{
    /// <summary>
    /// ���뵯����
    /// </summary>
    [SerializeField]
    public Image success;

    /// <summary>
    /// ����仯��ͼƬ
    /// </summary>
    [SerializeField]
    public Sprite img1, img2;

    /// <summary>
    /// ��ͼƬ��Ϊ�����У�����ʱ�����Ի���
    /// </summary>
    public void OnMouseDown()
    {
        if (this.GetComponent<Image>().sprite == img2) { }
        else
        { 
            this.GetComponent<Image>().sprite = img2;
            this.GetComponent<Button>().interactable = false;
            Invoke("ChangeBack", 2);
            Invoke("Success", 2);
        }
    }

    /// <summary>
    /// �����Ի���
    /// </summary>
    private void Success()
    {
        success.gameObject.SetActive(true);
    }

    /// <summary>
    /// ����ťͼƬ���Ļ�ȥ
    /// </summary>
    private void ChangeBack()
    {
        this.GetComponent<Image>().sprite = img1;
        this.GetComponent<Button>().interactable = true;
    }
}
