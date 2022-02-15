using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Recruiting : MonoBehaviour
   
{
    [Header("演员信息板")]
    public Image ActorImage;
    public Text ActorName;
    public Text Age;
    public Text Degree;
    public Image EntryImage;
    public Text EntryIntroduction;
    public Text Payment;
    public Text Salary;
    public Text Introduction;
    public Text Comefrom;
    public Text PerfectValue;
    public GameObject ImfoBoard;
    //演员列
    private int CurrentCharacterNum;//当前操作演员号
    public List<Character> RecruitCharacters;//演员应聘列表
    //广告
    public GameObject[] RecruitButton;//广告按钮
    public Sprite IsPost;//发布中
    //存档
    public PlayerController CurrentPlayerController;//当前存档
    //提示
    public GameObject Remaintext;
    public Text Property;
    //输入名字
    public GameObject InputNameBoard;
    private void Start()
    {
        CurrentPlayerController = MainController.mainController.CurrentPlayerController;
        UpdateRecruitList();
        UpdateButtonCondition();
        ShowImfo();
    }
    private void Update()
    {
        Property.text = "财富：" + CurrentPlayerController.Property + " ￥";
    }


    public void AfterAdvertising(int _advertiseNum)
    {
        int _advertisementPrice = 0;
        switch (_advertiseNum)
        {
            case 0:
                _advertisementPrice = 100;
                break;
            case 1:
                _advertisementPrice = 2000;
                break;
            case 2:
                _advertisementPrice = 10000;
                break;
            default:
                break;
        }
        if (CurrentPlayerController.Property >= _advertisementPrice)
        {
            CurrentPlayerController.RecruitList[_advertiseNum] = true;
            CurrentPlayerController.Property -= _advertisementPrice;//减广告费
            UpdateButtonCondition();
            MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Recruit"));
        }
        else
        {
            StartCoroutine(Remain(Remaintext,"余额不足，请充值！"));
        }

    }
    private void UpdateButtonCondition()
    {
        int i = 0;
        foreach (bool _item in CurrentPlayerController.RecruitList)
        {
            if (_item)
            {
                RecruitButton[i].GetComponent<Image>().sprite = IsPost;
                RecruitButton[i].GetComponent<Button>().interactable = false;
            }
            i++;
        }
    }
    /// <summary>
    /// 更新招聘列
    /// </summary>
    public void UpdateRecruitList()
    {
        Character[] gameObjects = MainController.mainController.CurrentPlayerController.RecruitCharacters;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Character gameObject = gameObjects[i];
            if (gameObject.PerfectValue != 0)
            {
                RecruitCharacters.Add(gameObject);
            }
        }
       
    }
    public void GiveupCharacter()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Button"));
        Character _character = RecruitCharacters[CurrentCharacterNum];
        RecruitCharacters.Remove(_character);//本招募列表去掉该演员
        MainController.mainController.CurrentPlayerController.RecruitCharacters[_character.CharacterLevel] = new Character();//主控招募列表去掉该演员
        if (CurrentCharacterNum > 0)
        {
            CurrentCharacterNum--;
        }
        ShowImfo();
        //Debug.Log("Assets/Resources/CharactersLibrary/File" + CurrentPlayerController.FileNum + "/" + (CurrentPlayerController.CreateCharacterNum - 1) + ".prefab");
        //Directory.Delete("Assets/Resources/CharactersLibrary/File" + CurrentPlayerController.FileNum + "/" + (CurrentPlayerController.CreateCharacterNum - 1) + ".prefab",true);
        //Resources.UnloadAsset(_character);
        //MonoBehaviour.DestroyImmediate(_character,true);
        //DestroyImmediate(_character, true) ;
    }
    public void RecruitCharacter()
    {
        Character _character = RecruitCharacters[CurrentCharacterNum];
        if (CurrentPlayerController.Property >= _character.EmployPrice)//判断是否有钱买
        {
            MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/RecruitAndBuy"));
            StartCoroutine(Remain(Remaintext, "签约成功！"));
            ShowInputBoard(_character);
        }
        else
        {
            StartCoroutine(Remain(Remaintext,"余额不足，请充值！"));
        }
    }

    public void LeftButton()
    {
        if (CurrentCharacterNum > 0)
        {
            CurrentCharacterNum--;
        }
        ShowImfo();
    }
    public void RightButton()
    {
        if (CurrentCharacterNum < RecruitCharacters.Count - 1)
        {
            CurrentCharacterNum++;
        }
        ShowImfo();
    }
    public void ShowImfo()
    {
        if (RecruitCharacters.Count != 0)//如果有招募人员
        {
            Character _character = RecruitCharacters[CurrentCharacterNum];
            ActorImage.sprite = _character.CharacterSprite;
            ActorName.text = _character.Name;
            Age.text = _character.Age;
            Degree.text = _character.Degree;
            EntryImage.sprite = _character.EntrySprite;
            EntryIntroduction.text = _character.EntryImfo;
            Payment.text = _character.EmployPrice.ToString() + " ￥";
            Salary.text = _character.Salary.ToString() + " ￥";
            Introduction.text = _character.Background;
            PerfectValue.text = "完美值：" +  _character.PerfectValue;
            Comefrom.text = _character.ComeFrom;
            ImfoBoard.SetActive(true);
            //
            
        }
        else
        {
            ImfoBoard.SetActive(false);
            //StartCoroutine(Remain(Remaintext,"暂无简历"));
        }
    }

    public void ShowInputBoard(Character _gameObject)
    {
        InputNameBoard.transform.GetChild(0).GetComponent<Image>().sprite = _gameObject.CharacterSprite;
        InputNameBoard.SetActive(true);
    }
    public void InputName()
    {
        string _NewName = InputNameBoard.transform.GetChild(1).GetComponent<InputField>().text;
        Character _character = RecruitCharacters[CurrentCharacterNum];
        Debug.Log(_NewName.Length);
        if (_NewName.Length > 0 && _NewName.Length <= 4) 
        {
            InputNameBoard.transform.GetChild(1).GetComponent<InputField>().text = "";
            _character.ChangeName(_NewName);
            CurrentPlayerController.AddCharacters(_character);//玩家演员库获得该演员
            CurrentPlayerController.Property -= (int)_character.EmployPrice;
            RecruitCharacters.Remove(_character);//招募列表去掉该演员
            InputNameBoard.SetActive(false);
            MainController.mainController.CurrentPlayerController.RecruitCharacters[_character.CharacterLevel] = new Character();
            StartCoroutine(Remain(Remaintext, "命名成功！"));
            CurrentPlayerController.ActorAdd++;

            if (CurrentCharacterNum > 0)
            {
                CurrentCharacterNum--;
            }
            ShowImfo();
        }
        else
        {
            InputNameBoard.transform.GetChild(1).GetComponent<InputField>().text = "";
            StartCoroutine(Remain(Remaintext, "阿欧，再想想吧"));
        }
    }
    
    IEnumerator Remain(GameObject _remainText,string _Text)
    {
        _remainText.transform.GetChild(0).GetComponent<Text>().text = _Text;
        _remainText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _remainText.SetActive(false);
    }
}
