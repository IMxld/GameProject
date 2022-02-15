using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadActors : MonoBehaviour
{
    private static LoadActors _loadActors;
    public static LoadActors loadActors
    {
        get
        {
            if (_loadActors != null)
            {
                return _loadActors;
            }
            else
            {
                _loadActors = Transform.FindObjectOfType<LoadActors>();
                return _loadActors;
            }
        }
    }
    [SerializeField]
    public GameObject content;
    public GameObject Card;//卡片
    List<Character> Characters;
    public GameObject ImfoBoard;//板子
    [Header("演员基本信息")]
    public Image ActorImage;
    public Text ActorName;
    public Text Age;
    public Text Degree;
    public Image EntryImage;
    public Text EntryIntroduction;
    public Text Payment;
    public Text Salary;
    public Text Introduction;
    public Text PerfectValue;
    public Text ComeFrom;


    private void Awake()
    {
        int i = 0;
        Characters = MainController.mainController.CurrentPlayerController.CharactersLibrary;
        for (int n = 0; n < Characters.Count/5 + 1; n++)
        {
            for(int m = 0; m < 5; m++)
            {
                try
                {
                    GameObject _card = Instantiate(Card);
                    ActorPointDown actorPointDown = _card.AddComponent<ActorPointDown>();
                    actorPointDown.character = Characters[i];
                    _card.transform.Translate(new Vector3(243 * (m + 1), -350 * n + -50)) ;
                    _card.transform.SetParent(gameObject.transform);
                    _card.transform.GetChild(0).gameObject.GetComponent<Text>().text = actorPointDown.character.Name;//演员名
                    _card.transform.GetChild(1).gameObject.GetComponent<Text>().text = "疲劳值：" + (int)actorPointDown.character.CurrentStamina + "/" + actorPointDown.character.MaxStamina;//演员疲劳值
                    _card.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = actorPointDown.character.EntrySprite;//演员词条图片
                    _card.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = actorPointDown.character.CharacterSprite;//演员图片
                    
                    i++;
                    
                }
                catch(ArgumentOutOfRangeException)
                {
                    break;
                }
            }
        }
    }

    public void LoadImfo(Character _character)
    {

        ActorImage.sprite = _character.CharacterSprite;
        ActorName.text = _character.Name;
        Age.text = "年龄：" +  _character.Age;
        Degree.text = "学历" + _character.Degree;
        EntryImage.sprite = _character.EntrySprite;
        EntryIntroduction.text = _character.EntryImfo;
        Payment.text = "签约费：" +  _character.EmployPrice.ToString() + " ￥";
        Salary.text = "周薪：" + _character.Salary.ToString() + " ￥";
        Introduction.text = _character.Background;
        PerfectValue.text = "完美值：" + _character.PerfectValue;
        ComeFrom.text = _character.ComeFrom;
        ImfoBoard.SetActive(true);
    }
}
