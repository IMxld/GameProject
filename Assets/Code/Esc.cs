using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esc : MonoBehaviour
{
    public Canvas canvas;
    public Image option;
    public GameObject boat;
    public Slider musicNum;
    public Slider voiceNum;
    public Slider allNum;

    public static int pageNum;

    private void Start()
    {
        pageNum = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pageNum == 0)
            {
                canvas.gameObject.SetActive(true);
                boat.SetActive(false);
                pageNum += 1;
            }
            else if (pageNum == 1)
            {
                canvas.gameObject.SetActive(false);
                boat.SetActive(true);
                pageNum -= 1;
            }
            else if (pageNum == 2)
            {
                Save.files.options.music = musicNum.value;
                Save.files.options.voice = voiceNum.value;
                Save.files.options.allMusic = allNum.value;
                Save.SaveJson();
                pageNum -= 1;
                option.gameObject.SetActive(false);
            }
        }
    }
}
