using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Absorb : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    /// <summary>
    /// 吸附触发范围
    /// </summary>
    [SerializeField]
    RectTransform triggerField;
    /// <summary>
    /// 已吸附点
    /// </summary>
    private GameObject AbsorbPoint;
    /// <summary>
    /// 获取该物体的当前位置
    /// </summary>
    private RectTransform thisPos;

    /// <summary>
    /// 引入吸附点列表
    /// </summary>
    [SerializeField]
    DragPointsList dragPointList;

    /// <summary>
    /// 记录物体原始坐标
    /// </summary>
    private Vector3 OriginalPos;
    /// <summary>
    /// 该演员在玩家演员库中的编号
    /// </summary>
    public int actorNum;
    /// <summary>
    /// 该演员在当前卡片中的编号
    /// </summary>
    public int currentActorNum;
    /// <summary>
    /// 该演员吸附框的编号
    /// </summary>
    private int FrameNum;
    /// <summary>
    /// 当前操作剧本
    /// </summary>
    private MainDrama CurrentDrama;
    /// <summary>
    /// 存档对应角色信息
    /// </summary>
    private Character CurrentCharacter;
    /// <summary>
    /// 
    /// </summary>
    private PlayerController CurrentController;
    public int PageNum;

    public void AfterStart()
    {
        thisPos = this.GetComponent<RectTransform>();
        OriginalPos = thisPos.position;
        this.dragPointList = DragPointsList.dragPointsList;
        triggerField = dragPointList.GetComponent<RectTransform>();
        CurrentController = MainController.mainController.CurrentPlayerController;
        CurrentCharacter = CurrentController.CharactersLibrary[actorNum];//当前操作角色
    }


    /// <summary>
    /// 计算当前位置
    /// </summary>
    /// <param name="eventData">事件数据</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (IsSamePage())
        {
            var movePos = thisPos.anchoredPosition + eventData.delta;
            var curPos = new Vector2(movePos.x, movePos.y);
            thisPos.anchoredPosition = curPos;
        }

    }

    /// <summary>
    /// 如果按钮落在识别范围内，则吸附，否则回到原位
    /// </summary>
    /// <param name="eventData">事件数据</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsSamePage()) return;//如果不在同一页
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/PutDownActor"));
        if (CanPlay())//如果体力支持
        {
            CurrentDrama = MainController.mainController.CurrentDrama.GetComponent<MainDrama>();//当前操作剧本
            if (Mathf.Abs(thisPos.position.x - triggerField.transform.position.x) < triggerField.rect.width / 2
                && Mathf.Abs(thisPos.position.y - triggerField.transform.position.y) < triggerField.rect.height / 2
                && CanAbsorb() && !CurrentCharacter.IsPlaying)
            {
                thisPos.position = dragPointList.GetNearestPoint(thisPos.position);
                AbsorbPoint = dragPointList.minOne;
                FrameNum = dragPointList.actorDic[AbsorbPoint.name];//获取吸附框编号
                LoadCharacter(CurrentCharacter, FrameNum);//将存档中对应演员置入剧本角色之中
                CurrentCharacter.LoadDrama(MainController.mainController.CurrentDrama);//演员添加剧本信息
                CurrentCharacter.IsPlaying = true;//演员参演
                dragPointList.actorPoints.Remove(AbsorbPoint);//将已吸附的框锁住
                LoadCharacterName(CurrentCharacter.Name);
                ChangeSize(300, 300);
                LoadSprite(CurrentCharacter.CharacterChooseSprite);
                CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(3).gameObject.SetActive(true);//选中
                CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(4).gameObject.SetActive(true);//选中
            }
            else
            {
                //回到原位
                thisPos.position = OriginalPos;
            }
        }
        else
        {
            //回到原位
            thisPos.position = OriginalPos;
            //提示体力不足
            StartCoroutine(Remain(CharactersListPreferance.charactersListPreferance.RemainText)) ;
        }
    }
    private bool CanPlay()
    {
        Character character = CurrentCharacter;
        if (character.CurrentStamina + character.StaminaDecreaseSpeed >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsSamePage()
    {
        if(CharactersListPreferance.charactersListPreferance.CurrentPageNum == PageNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CanAbsorb()
    {
        if (dragPointList.actorPoints.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void LoadCharacterName(string _name)
    {
        AbsorbPoint.transform.GetChild(3).GetComponent<Text>().text = _name;
    }
    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="_sprite"></param>
    private void LoadSprite(Sprite _sprite)
    {
        gameObject.GetComponent<Image>().sprite = _sprite;
    }
    /// <summary>
    /// 在开始拖动的一瞬间检测演员是否在原位，否则先将所占坑位补回去，避免出现同一演员占多个坑位的情况
    /// </summary>
    /// <param name="eventData">事件数据</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/CatchActor"));
        if (CurrentCharacter.IsPlaying && IsSamePage())
        {
            dragPointList.actorPoints.Add(AbsorbPoint);
            LoadCharacter(null, FrameNum);
            ChangeSize(310,420);
            LoadCharacterName("");
            LoadSprite(CurrentCharacter.CharacterSprite);
            CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(3).gameObject.SetActive(false);//取消
            CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(4).gameObject.SetActive(false);//取消
            CurrentCharacter.IsPlaying = false;//演员退演
        }
    }
    /// <summary>
    /// 将演员信息载入到剧本所对应角色框中
    /// </summary>
    /// <param name="_character"></param>
    /// <param name="_targetNum"></param>
    private void LoadCharacter(Character _character, int _targetNum)
    {
        CurrentDrama.CharactersList[_targetNum] = _character;
    }

    /// <summary>
    /// 改变大小
    /// </summary>
    /// <param name="_targetX"></param>
    /// <param name="_targetY"></param>
    private void ChangeSize(float _targetX, float _targetY)
    {
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _targetX);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _targetY);
    }

    IEnumerator Remain(GameObject _remainText)
    {
        _remainText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _remainText.SetActive(false);
    }
}
