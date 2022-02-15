using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [Header("basic Imfomation")]
    [Space]
    public string Name;//名字
    public string Degree;//学历
    public string Age;//年龄
    [TextArea]
    public string Background;//背景故事
    public int CharacterLevel;//角色等级
    public string ComeFrom;//来自
    [Header("entry")]
    [Space]
    public IEntryInterface Entry;//词条
    public string EntryName;//词条名
    public string EntryImfo;//词条说明
    public string EntryPool;//词条池子
    public int EntryNum;//词条编号
    public Sprite EntrySprite;//词条图片
    private int EntryMax;//总词条数
    [Space]
    public float MaxStamina = 100;//体力上限
    public float CurrentStamina = 100;//当前体力值
    public float StaminaDecreaseSpeed = -30;//体力损耗速度/演剧
    public float StaminaRecoverSpeed = 15;//体力回复速度/不演剧
    [Space]
    public float EmployPrice;//雇佣金
    public float Salary;//周薪
    public float PerfectValue = 0;//完美值
    public List<float> VaribleBackup;//变量初始值备份
    /// <summary>
    /// 是否参与表演
    /// </summary>
    public bool IsPlaying = false;
    /// <summary>
    /// 角色立绘
    /// </summary>
    public Sprite CharacterSprite;
    /// <summary>
    /// 角色选择框
    /// </summary>
    public Sprite CharacterChooseSprite;
    public MainDrama CurrentDrama;//获取当前剧本
    public PlayerController CurrentPlayerController;//获取当前存档

    /// <summary>
    /// 演员初始化
    /// </summary>
    /// <param name="_level"></param>
    /// <param name="_currentPlayerController"></param>
    public void CharacterStart(int _level,PlayerController _currentPlayerController)
    {
        CurrentPlayerController = _currentPlayerController;
        CharacterLevel = _level;
        GetComeFrom(_level);
        GetEntry(_level);//获得词条
        GetSalary(_level);//随机确定工资和雇佣金
        GetPerfectValue();//获得完美值
        Entry.OriginalEffect(this);//如果词条能力影响终身，则启动
        RemarkVarible();
    }

    private void RemarkVarible()//记录各个可变数值
    {
        VaribleBackup = new List<float>();
        VaribleBackup.Add(StaminaDecreaseSpeed);//0
        VaribleBackup.Add(StaminaRecoverSpeed);//1
        VaribleBackup.Add(Salary);//2
        VaribleBackup.Add(PerfectValue);//3

    }
    public void InitializedVariable()//还原被修改的数值
    {
        StaminaDecreaseSpeed = VaribleBackup[0];
        StaminaRecoverSpeed = VaribleBackup[1];
        Salary = VaribleBackup[2];
        PerfectValue = VaribleBackup[3];
        IsPlaying = false;
    }


    public void UpdateStaminaDecreaseSpeed(int _actAccount)//根据剧本幕数更新体力消耗速度
    {
        StaminaDecreaseSpeed *= 1 + _actAccount/10;
    }
    public void DecreaseStamita()//减少体力值
    {
        if (CurrentStamina + StaminaDecreaseSpeed < 0)
        {
            
        }
        else
        {
            CurrentStamina += StaminaDecreaseSpeed;
        }
    }
    public void RecoverStamita()//恢复体力值
    {
        if (CurrentStamina + StaminaRecoverSpeed > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        else
        {
            CurrentStamina += StaminaRecoverSpeed;
        }
    }
    public void ChangeName(string _newName)//改名字
    {
        Name = _newName;
    }
    private void GetComeFrom(int _level)
    {
        switch (_level)
        {
            case 0:
                ComeFrom = "朋友圈黑工";
                break;
            case 1:
                ComeFrom = "街边素人";
                break;
            case 2:
                ComeFrom = "北影上戏名流";
                break;
            default:
                break;
        }
    }

    private void GetEntry(int _level)//从池子随机抽取词条
    {
        float _bottomRate;//抽到普通词条概率
        float _middleRate;//冲到高级词条概率
        float _topRate;//抽到传说词条概率
        float _rate = Random.Range(0f, 1f);
        EntryPool _entryPool = null;
        switch (_level)
        {
            case 0:
                _bottomRate = 0.95f;
                _middleRate = 0.045f;
                _topRate = 0.005f;
                break;
            case 1:
                _bottomRate = 0.7f;
                _middleRate = 0.25f;
                _topRate = 0.05f;
                break;
            default:
                _bottomRate = 0.4f;
                _middleRate = 0.35f;
                _topRate = 0.25f;
                break;
        }
        
        if (_rate <= _bottomRate)
        {
            _entryPool = Resources.Load<EntryPool>("Entry Pool1");//获取普通词条资源库
            EntryPool = "Entry Pool1";
        }
        else if (_bottomRate<_rate && _rate <= 1 - _topRate)
        {
            _entryPool = Resources.Load<EntryPool>("Entry Pool2");//获取高级词条资源库
            EntryPool = "Entry Pool2";
        }
        else if (_bottomRate + _middleRate < _rate)
        {
            _entryPool = Resources.Load<EntryPool>("Entry Pool3");//获取传说词条资源库
            EntryPool = "Entry Pool3";
        }
        EntryMax = _entryPool.entries.Count;//获取词条库上限
        EntryNum =(int) Random.Range(0, EntryMax);//随机获取词条编号
        Entry = _entryPool.entries[EntryNum].GetComponent<IEntryInterface>() ;//根据词条编号获取词条
        EntryName = Entry.ReturnName();//获得词条名
        EntryImfo = Entry.ReturnImfo();//获得词条词缀
        EntrySprite = Entry.ReturnSprite();//获得词条图片
    } 

    public void GetSalary(int _level)//确定周薪和雇佣金
    {
        switch (_level)
        {
            case 0:
                Salary = (int)Random.Range(10, 40);
                EmployPrice = (int)Random.Range(50, 100);
                break;
            case 1:
                Salary = (int)Random.Range(200, 300);
                EmployPrice = (int)Random.Range(500, 800);
                break;
            case 2:
                Salary = (int)Random.Range(1000, 2000);
                EmployPrice = (int)Random.Range(2000, 5000);
                break;
            default:
                break;
        }
    }


    private void GetPerfectValue()//随机抽取完美值
    {
        PerfectValue = Random.Range(30, 200);
    }
    public void AfterEffect()//如果词条能力影响结算，则启动
    {
        Entry.AfterEffect(this);
    }
    public void PerfectEffect()//如果完美时会影响，则启动
    {
        Entry.PerfectEffect(this);
    }
    public void WeekEffect()//如果词条能力影响每周，则启动
    {
        Entry.WeekEffect(this);
    }


    public void LoadDrama(GameObject _drama)//附加剧本
    {
        CurrentDrama = _drama.GetComponent<MainDrama>();
    }

    public void ReflashEntry()
    {
        EntryPool _entryPool = Resources.Load<EntryPool>(EntryPool);
        Entry = _entryPool.entries[EntryNum].GetComponent<IEntryInterface>();
    }
}
