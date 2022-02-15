using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotoOption : MonoBehaviour
{
    public Image option;
    public Slider musicNum;
    public Slider voiceNum;
    public Slider allNum;

    public void OnMouseDown()
    {
        musicNum.value = Save.files.options.music;
        voiceNum.value = Save.files.options.voice;
        allNum.value = Save.files.options.allMusic;
        Esc.pageNum += 1;
        option.gameObject.SetActive(true);
        //this.transform.localScale -= new Vector3(0.1f, 0.1f);
    }
}
