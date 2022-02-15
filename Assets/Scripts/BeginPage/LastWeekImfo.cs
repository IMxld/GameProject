using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastWeekImfo : MonoBehaviour
{
    public Text money;
    public Text fans;
    public Text playNum;
    public Text actorAdd;
    public Text actorPay;
    public Text routinePay;
    public Text audience;
    public Text dramaAdd;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController currentPlayerController = MainController.mainController.CurrentPlayerController;
        money.text = "���룺+ " + currentPlayerController.LastWeekIncome + " ��";
        fans.text = "��˿����+ " + currentPlayerController.LastWeekFans;
        playNum.text = "�ݳ������� " + currentPlayerController.LastWeekPlayNum + " ��";
        actorAdd.text = "��Ա����+ " + currentPlayerController.LastWeekActorAdd;
        dramaAdd.text = "�籾����+ " + currentPlayerController.LastWeekDrama;
        audience.text = "����������" + currentPlayerController.LastWeekAudience;
        actorPay.text = "��Ա������- " + currentPlayerController.LastWeekActorPay;
        routinePay.text = "�ӷ�֧����- " + currentPlayerController.LastWeekRoutinePay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
