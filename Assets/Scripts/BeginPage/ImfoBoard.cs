using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImfoBoard : MonoBehaviour
{
    public Text WeekCount;//��
    public Text Property;//Ǯ
    public Text Fans;//��˿
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        Property.text = MainController.mainController.CurrentPlayerController.Property.ToString();
        Fans.text = MainController.mainController.CurrentPlayerController.FansCount.ToString();
        WeekCount.text = MainController.mainController.CurrentPlayerController.WeekTime.ToString();
    }
}
