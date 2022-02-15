using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookImfoBoard : MonoBehaviour
{
    public Image BookPost;//剧本封面
    public Text BookName;//剧本名字
    public Text BookType;//剧本类型
    public Text BookIntroduction;//剧本简介
    public GameObject BookContent;//剧本内容

    public void LoadDramaImfo(MainDrama _mainDrama)
    {
        BookPost.sprite = _mainDrama.ImageBackground;
        BookName.text = _mainDrama.Name;
        BookType.text = "类型：" + _mainDrama.Type;
        BookIntroduction.text = _mainDrama.Plot;
        BookContent.GetComponent<Text>().text = _mainDrama.Dialog.ToString();
    }
}
