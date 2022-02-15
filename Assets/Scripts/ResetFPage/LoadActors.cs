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
    public GameObject Card;//��Ƭ
    List<Character> Characters;
    public GameObject ImfoBoard;//����
    [Header("��Ա������Ϣ")]
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
                    _card.transform.GetChild(0).gameObject.GetComponent<Text>().text = actorPointDown.character.Name;//��Ա��
                    _card.transform.GetChild(1).gameObject.GetComponent<Text>().text = "ƣ��ֵ��" + (int)actorPointDown.character.CurrentStamina + "/" + actorPointDown.character.MaxStamina;//��Աƣ��ֵ
                    _card.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = actorPointDown.character.EntrySprite;//��Ա����ͼƬ
                    _card.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = actorPointDown.character.CharacterSprite;//��ԱͼƬ
                    
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
        Age.text = "���䣺" +  _character.Age;
        Degree.text = "ѧ��" + _character.Degree;
        EntryImage.sprite = _character.EntrySprite;
        EntryIntroduction.text = _character.EntryImfo;
        Payment.text = "ǩԼ�ѣ�" +  _character.EmployPrice.ToString() + " ��";
        Salary.text = "��н��" + _character.Salary.ToString() + " ��";
        Introduction.text = _character.Background;
        PerfectValue.text = "����ֵ��" + _character.PerfectValue;
        ComeFrom.text = _character.ComeFrom;
        ImfoBoard.SetActive(true);
    }
}
