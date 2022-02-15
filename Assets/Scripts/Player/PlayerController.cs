using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
[System.Serializable]
public class PlayerController 
{
    /// <summary>
    /// ��������Ա��
    /// </summary>
    public int CreateCharacterNum;
    /// <summary>
    /// �ڼ����¿���
    /// </summary>
    public int FileNum;
    public int OriginalCharactersCount = 2;//��ʼ��Ա��
    public int Property = 500;//���ʲ�
    public int FansCount = 0;//��˿��
    public int RoutinePayment;//ÿ���ӷ�
    public int WeekTime = 1;//����
    public List<Character> CharactersLibrary = new List<Character>();//��Ա����
    public List<GameObject> DramaLibrary = new List<GameObject>();//�籾����
    private int TargetProperty = 200000;//Ŀ���ʲ�
    private int MaxWeek = 20;//��������
    public bool[] RecruitList = new bool[3];
    /// <summary>
    /// ��ԱӦƸ�б�
    /// </summary>
    public Character[] RecruitCharacters = new Character[3];
    /// <summary>
    /// �浵����
    /// </summary>
    public string FileTime;
    //������ˮ
    public int LastWeekPlayNum;//
    public float LastWeekIncome;//
    public float LastWeekFans;//
    public float LastWeekAudience;//
    /// <summary>
    ///  ������Ա++
    /// </summary>
    public int LastWeekActorAdd;//
    public int ActorAdd;
    public int LastWeekActorPay;
    public int LastWeekRoutinePay;//
    /// <summary>
    ///  ����籾++
    /// </summary>
    public int LastWeekDrama;
    public int DramaAdd;



    public void StartNewGame()//��ʼ�´浵
    {

        for (int i = 0; i < OriginalCharactersCount; i++)
        {
            CharactersPool _charactersPool = Resources.Load<CharactersPool>("Characters Pool");
            Character _son = new Character();
            Character _mom = _charactersPool.Characters[UnityEngine.Random.Range(0, _charactersPool.Characters.Count)];
            MainController.mainController.ActorTranlate(_mom, _son);
            _son.CharacterStart(0,this);
            CharactersLibrary.Add(_son) ;
        }

    }
    public void AddCharacters(Character _character)//�����Ա
    {
        CharactersLibrary.Add(_character);
    }

    public void AddDramaLibrary(GameObject _drama)//��Ӿ籾
    {
        DramaLibrary.Add(_drama);
    }



    private void RoutinePay()//֧���ӷ�
    {
        RoutinePayment = LastWeekRoutinePay = CharactersLibrary.Count * 10 + DramaLibrary.Count * 50;//�籾��*50 + ��Ա��*10
        Property -= RoutinePayment;
    }
    
    public void WeekGone()//һ�ܹ�ȥ
    {
        WeekTime++;
        RoutinePay();
        LastWeekActorPay = 0;
        foreach (Character _item in CharactersLibrary)//������Ա�Ⲣ�۳���Ա����
        {
            _item.ReflashEntry();
            _item.WeekEffect();
            Property -=(int) _item.Salary;
            LastWeekActorPay += (int)_item.Salary;
        }
        WeekRemark();
        InitializeRecruitList();
        GameOver();//�鿴��û�������
    }
    
    public void WeekRemark()
    {
        LastWeekActorAdd = ActorAdd;
        ActorAdd = 0;
        //
        LastWeekDrama = DramaAdd;
        DramaAdd = 0;  
    }

    /// <summary>
    /// ������ļ�б�
    /// </summary>
    private void InitializeRecruitList()
    {
        for (int i = 0; i < RecruitList.Length; i++)
        {
            RecruitList[i] = false;
        }
    }

    public void GameOver()
    {
        if (WeekTime > MaxWeek)
        {
            if (Property < TargetProperty)
            {
                Debug.Log("you are loser");
            }
            else
            {
                Debug.Log("congratulation");
            }
        }

    }

    /// <summary>
    /// ��ȡ�浵ʱ��
    /// </summary>
    public void GetFileTime()
    {
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;
        int year = DateTime.Now.Year;
        int mouth = DateTime.Now.Month;
        int day = DateTime.Now.Day;
        string currentTime = string.Format("{0:D2}/{1:D2}/{2:D2}" + " {3:D2}:{4:D2}:{5:D2}", year, mouth, day, hour, minute, second);
        FileTime = currentTime;
    }
}

