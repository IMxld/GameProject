using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharactersListPreferance : MonoBehaviour
{
    public int CurrentPageNum;//ҳ��
    public int MaxPageNum;//���ҳ��
    private int CharacterCount;//��Ա��
    private int CurrentCharacterCount;//��ǰ��ʹ����Ա��
    public GameObject RemainText;
    public GameObject[] Cards;
    public GameObject[] CurrentCharacters = new GameObject[5];//��Ա����
    public List<Character> CharactersList;
    public List<GameObject> ActorsObj;
    public GameObject actor;
    private static CharactersListPreferance _charactersListPreferance;
    public static CharactersListPreferance charactersListPreferance
    {
        get
        {
            if (_charactersListPreferance != null)
            {
                return _charactersListPreferance;
            }
            else
            {
                _charactersListPreferance = Transform.FindObjectOfType<CharactersListPreferance>();
                return _charactersListPreferance;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CharacterCount = MainController.mainController.CurrentPlayerController.CharactersLibrary.Count;
        InstantiateCharacters();
        MaxPageNum = CurrentCharacterCount / 5;
        InstantiateCharacterCard();
    }

    IEnumerator Initialize(GameObject gameObject)
    {
        yield return new WaitForFixedUpdate();
        gameObject.GetComponent<Absorb>().AfterStart();
    }

    private void InstantiateCharacters()
    {
        CurrentCharacterCount = CharacterCount;
        int t = 0;
        for (int i = 0; i < CharacterCount; i++)//һ��ʼ������Ա��
        {
            if (MainController.mainController.CurrentPlayerController.CharactersLibrary[i].IsPlaying)
            {
                CurrentCharacterCount--;
            }
            else
            {

                Character character = MainController.mainController.CurrentPlayerController.CharactersLibrary[i];
                GameObject _card = Cards[t % 5];
                GameObject _item = Instantiate(actor, _card.transform);//������Ա
                ActorsObj.Add(_item);
                Absorb absorb = _item.AddComponent<Absorb>();
                _item.transform.SetParent(transform);
                absorb.PageNum = (int)t / 5;//��Ա����ҳ��
                CharactersList.Add(character);
                _item.GetComponent<RectTransform>().position = new Vector3(_card.transform.position.x, _card.transform.position.y + 35);
                //_item.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 0);
                _item.GetComponent<Image>().sprite = character.CharacterSprite;
                _item.SetActive(false);
                absorb.actorNum = i;
                absorb.currentActorNum = t;
                StartCoroutine(Initialize(_item));
                t++;
                

            }
            

        }
    }
    /// <summary>
    /// ������Ա��Ƭ
    /// </summary>
    ///
    public void InstantiateCharacterCard() 
    {
        int _firstNum;
        int _lastNum;
        if (CurrentPageNum == MaxPageNum)
        {
            _firstNum = CurrentPageNum * 5;
            _lastNum = CurrentCharacterCount;
        }
        else
        {
            _firstNum = CurrentPageNum * 5;
            _lastNum = (CurrentPageNum + 1) * 5;
        }
        for (int i = _firstNum; i < _lastNum; i++)
        {
            Character _item = CharactersList[i];//������Ա
            GameObject card = Cards[i % 5] ;//��Ƭ
            card.SetActive(true);
            ActorsObj[i].SetActive(true);
            card.transform.GetChild(0).gameObject.GetComponent<Text>().text = _item.Name;//��Ա��
            card.transform.GetChild(1).gameObject.GetComponent<Text>().text = "ƣ��ֵ��" + (int)_item.CurrentStamina + "/" + _item.MaxStamina;//��Աƣ��ֵ
            card.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = _item.EntrySprite;//��Ա����ͼƬ
            card.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(255, 255, 255);//���ɫ
            card.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = _item.CharacterSprite;//��ԱͼƬ
            if (_item.IsPlaying)//�����ѡ�У�����ʾ��ѡ��
            {
                card.transform.GetChild(3).gameObject.SetActive(true);
                card.transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                card.transform.GetChild(3).gameObject.SetActive(false);
                card.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
    public void LeftButton()
    {
        if (CurrentPageNum == 0) return;
        ClearCard();
        CurrentPageNum--;
        InstantiateCharacterCard();

    }
    public void RightButton()
    {
        if (CurrentPageNum == MaxPageNum) return;
        ClearCard();
        CurrentPageNum++;
        InstantiateCharacterCard();
    }

    private void ClearCard()
    {
        for (int i = 0; i < CharactersList.Count; i++)
        {
            GameObject _gameObject = ActorsObj[i];
            Character character = CharactersList[i];
            GameObject _card = Cards[i % 5] ;
            _card.SetActive(false);
            if (!character.IsPlaying)
            {
                _gameObject.SetActive(false);
            }

        }
        
    }
}
