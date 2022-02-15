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
    /// ����������Χ
    /// </summary>
    [SerializeField]
    RectTransform triggerField;
    /// <summary>
    /// ��������
    /// </summary>
    private GameObject AbsorbPoint;
    /// <summary>
    /// ��ȡ������ĵ�ǰλ��
    /// </summary>
    private RectTransform thisPos;

    /// <summary>
    /// �����������б�
    /// </summary>
    [SerializeField]
    DragPointsList dragPointList;

    /// <summary>
    /// ��¼����ԭʼ����
    /// </summary>
    private Vector3 OriginalPos;
    /// <summary>
    /// ����Ա�������Ա���еı��
    /// </summary>
    public int actorNum;
    /// <summary>
    /// ����Ա�ڵ�ǰ��Ƭ�еı��
    /// </summary>
    public int currentActorNum;
    /// <summary>
    /// ����Ա������ı��
    /// </summary>
    private int FrameNum;
    /// <summary>
    /// ��ǰ�����籾
    /// </summary>
    private MainDrama CurrentDrama;
    /// <summary>
    /// �浵��Ӧ��ɫ��Ϣ
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
        CurrentCharacter = CurrentController.CharactersLibrary[actorNum];//��ǰ������ɫ
    }


    /// <summary>
    /// ���㵱ǰλ��
    /// </summary>
    /// <param name="eventData">�¼�����</param>
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
    /// �����ť����ʶ��Χ�ڣ�������������ص�ԭλ
    /// </summary>
    /// <param name="eventData">�¼�����</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsSamePage()) return;//�������ͬһҳ
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/PutDownActor"));
        if (CanPlay())//�������֧��
        {
            CurrentDrama = MainController.mainController.CurrentDrama.GetComponent<MainDrama>();//��ǰ�����籾
            if (Mathf.Abs(thisPos.position.x - triggerField.transform.position.x) < triggerField.rect.width / 2
                && Mathf.Abs(thisPos.position.y - triggerField.transform.position.y) < triggerField.rect.height / 2
                && CanAbsorb() && !CurrentCharacter.IsPlaying)
            {
                thisPos.position = dragPointList.GetNearestPoint(thisPos.position);
                AbsorbPoint = dragPointList.minOne;
                FrameNum = dragPointList.actorDic[AbsorbPoint.name];//��ȡ��������
                LoadCharacter(CurrentCharacter, FrameNum);//���浵�ж�Ӧ��Ա����籾��ɫ֮��
                CurrentCharacter.LoadDrama(MainController.mainController.CurrentDrama);//��Ա��Ӿ籾��Ϣ
                CurrentCharacter.IsPlaying = true;//��Ա����
                dragPointList.actorPoints.Remove(AbsorbPoint);//���������Ŀ���ס
                LoadCharacterName(CurrentCharacter.Name);
                ChangeSize(300, 300);
                LoadSprite(CurrentCharacter.CharacterChooseSprite);
                CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(3).gameObject.SetActive(true);//ѡ��
                CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(4).gameObject.SetActive(true);//ѡ��
            }
            else
            {
                //�ص�ԭλ
                thisPos.position = OriginalPos;
            }
        }
        else
        {
            //�ص�ԭλ
            thisPos.position = OriginalPos;
            //��ʾ��������
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
    /// ����ͼƬ
    /// </summary>
    /// <param name="_sprite"></param>
    private void LoadSprite(Sprite _sprite)
    {
        gameObject.GetComponent<Image>().sprite = _sprite;
    }
    /// <summary>
    /// �ڿ�ʼ�϶���һ˲������Ա�Ƿ���ԭλ�������Ƚ���ռ��λ����ȥ���������ͬһ��Առ�����λ�����
    /// </summary>
    /// <param name="eventData">�¼�����</param>
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
            CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(3).gameObject.SetActive(false);//ȡ��
            CharactersListPreferance.charactersListPreferance.Cards[currentActorNum % 5].transform.GetChild(4).gameObject.SetActive(false);//ȡ��
            CurrentCharacter.IsPlaying = false;//��Ա����
        }
    }
    /// <summary>
    /// ����Ա��Ϣ���뵽�籾����Ӧ��ɫ����
    /// </summary>
    /// <param name="_character"></param>
    /// <param name="_targetNum"></param>
    private void LoadCharacter(Character _character, int _targetNum)
    {
        CurrentDrama.CharactersList[_targetNum] = _character;
    }

    /// <summary>
    /// �ı��С
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
