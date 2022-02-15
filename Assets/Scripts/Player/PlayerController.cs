using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
[System.Serializable]
public class PlayerController 
{
    /// <summary>
    /// 已生成演员数
    /// </summary>
    public int CreateCharacterNum;
    /// <summary>
    /// 第几个新开档
    /// </summary>
    public int FileNum;
    public int OriginalCharactersCount = 2;//初始演员数
    public int Property = 500;//总资产
    public int FansCount = 0;//粉丝数
    public int RoutinePayment;//每日杂费
    public int WeekTime = 1;//周数
    public List<Character> CharactersLibrary = new List<Character>();//演员名单
    public List<GameObject> DramaLibrary = new List<GameObject>();//剧本名单
    private int TargetProperty = 200000;//目标资产
    private int MaxWeek = 20;//上限期限
    public bool[] RecruitList = new bool[3];
    /// <summary>
    /// 演员应聘列表
    /// </summary>
    public Character[] RecruitCharacters = new Character[3];
    /// <summary>
    /// 存档日期
    /// </summary>
    public string FileTime;
    //上周流水
    public int LastWeekPlayNum;//
    public float LastWeekIncome;//
    public float LastWeekFans;//
    public float LastWeekAudience;//
    /// <summary>
    ///  计算演员++
    /// </summary>
    public int LastWeekActorAdd;//
    public int ActorAdd;
    public int LastWeekActorPay;
    public int LastWeekRoutinePay;//
    /// <summary>
    ///  计算剧本++
    /// </summary>
    public int LastWeekDrama;
    public int DramaAdd;



    public void StartNewGame()//开始新存档
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
    public void AddCharacters(Character _character)//添加演员
    {
        CharactersLibrary.Add(_character);
    }

    public void AddDramaLibrary(GameObject _drama)//添加剧本
    {
        DramaLibrary.Add(_drama);
    }



    private void RoutinePay()//支付杂费
    {
        RoutinePayment = LastWeekRoutinePay = CharactersLibrary.Count * 10 + DramaLibrary.Count * 50;//剧本数*50 + 演员数*10
        Property -= RoutinePayment;
    }
    
    public void WeekGone()//一周过去
    {
        WeekTime++;
        RoutinePay();
        LastWeekActorPay = 0;
        foreach (Character _item in CharactersLibrary)//遍历演员库并扣除演员工资
        {
            _item.ReflashEntry();
            _item.WeekEffect();
            Property -=(int) _item.Salary;
            LastWeekActorPay += (int)_item.Salary;
        }
        WeekRemark();
        InitializeRecruitList();
        GameOver();//查看有没达成任务
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
    /// 重置招募列表
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
    /// 获取存档时间
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

