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
        money.text = "收入：+ " + currentPlayerController.LastWeekIncome + " ￥";
        fans.text = "粉丝量：+ " + currentPlayerController.LastWeekFans;
        playNum.text = "演出次数： " + currentPlayerController.LastWeekPlayNum + " 次";
        actorAdd.text = "演员数：+ " + currentPlayerController.LastWeekActorAdd;
        dramaAdd.text = "剧本数：+ " + currentPlayerController.LastWeekDrama;
        audience.text = "观众人数：" + currentPlayerController.LastWeekAudience;
        actorPay.text = "演员开销：- " + currentPlayerController.LastWeekActorPay;
        routinePay.text = "杂费支出：- " + currentPlayerController.LastWeekRoutinePay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
