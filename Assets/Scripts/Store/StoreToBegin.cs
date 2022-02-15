using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreToBegin : MonoBehaviour
{
    /// <summary>
    /// 引入变化的图片
    /// </summary>
    [SerializeField]
    public Sprite img1, img2;

    /// <summary>
    /// 从商店界面读取开始场景"BeginPage"
    /// </summary>
    public void OnMouseDown()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Back"));
        SceneManager.LoadScene("BeginPage");
    }

    /// <summary>
    /// 鼠标进入按钮时触发，更改图片
    /// </summary>
    public void OnMouseEnter()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Enter"));
        this.transform.localScale += new Vector3(0.1f, 0.1f);
        this.GetComponent<Image>().sprite = img2;
    }

    /// <summary>
    /// 鼠标离开按钮时触发，更改图片
    /// </summary>
    public void OnMouseExit()
    {
        this.transform.localScale -= new Vector3(0.1f, 0.1f);
        this.GetComponent<Image>().sprite = img1;
    }
}
