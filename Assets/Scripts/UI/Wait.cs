using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : MonoBehaviour
{
    /// <summary>
    /// 导入弹出框
    /// </summary>
    [SerializeField]
    public Image success;

    /// <summary>
    /// 引入变化的图片
    /// </summary>
    [SerializeField]
    public Sprite img1, img2;

    /// <summary>
    /// 将图片改为发布中，并延时弹出对话框
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
    /// 弹出对话框
    /// </summary>
    private void Success()
    {
        success.gameObject.SetActive(true);
    }

    /// <summary>
    /// 将按钮图片更改回去
    /// </summary>
    private void ChangeBack()
    {
        this.GetComponent<Image>().sprite = img1;
        this.GetComponent<Button>().interactable = true;
    }
}
