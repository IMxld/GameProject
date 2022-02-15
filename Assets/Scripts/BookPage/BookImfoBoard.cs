using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookImfoBoard : MonoBehaviour
{
    public Image BookPost;//�籾����
    public Text BookName;//�籾����
    public Text BookType;//�籾����
    public Text BookIntroduction;//�籾���
    public GameObject BookContent;//�籾����

    public void LoadDramaImfo(MainDrama _mainDrama)
    {
        BookPost.sprite = _mainDrama.ImageBackground;
        BookName.text = _mainDrama.Name;
        BookType.text = "���ͣ�" + _mainDrama.Type;
        BookIntroduction.text = _mainDrama.Plot;
        BookContent.GetComponent<Text>().text = _mainDrama.Dialog.ToString();
    }
}
