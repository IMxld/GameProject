using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [Header("basic Imfomation")]
    [Space]
    public string Name;//����
    public string Degree;//ѧ��
    public string Age;//����
    [TextArea]
    public string Background;//��������
    public int CharacterLevel;//��ɫ�ȼ�
    public string ComeFrom;//����
    [Header("entry")]
    [Space]
    public IEntryInterface Entry;//����
    public string EntryName;//������
    public string EntryImfo;//����˵��
    public string EntryPool;//��������
    public int EntryNum;//�������
    public Sprite EntrySprite;//����ͼƬ
    private int EntryMax;//�ܴ�����
    [Space]
    public float MaxStamina = 100;//��������
    public float CurrentStamina = 100;//��ǰ����ֵ
    public float StaminaDecreaseSpeed = -30;//��������ٶ�/�ݾ�
    public float StaminaRecoverSpeed = 15;//�����ظ��ٶ�/���ݾ�
    [Space]
    public float EmployPrice;//��Ӷ��
    public float Salary;//��н
    public float PerfectValue = 0;//����ֵ
    public List<float> VaribleBackup;//������ʼֵ����
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public bool IsPlaying = false;
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public Sprite CharacterSprite;
    /// <summary>
    /// ��ɫѡ���
    /// </summary>
    public Sprite CharacterChooseSprite;
    public MainDrama CurrentDrama;//��ȡ��ǰ�籾
    public PlayerController CurrentPlayerController;//��ȡ��ǰ�浵

    /// <summary>
    /// ��Ա��ʼ��
    /// </summary>
    /// <param name="_level"></param>
    /// <param name="_currentPlayerController"></param>
    public void CharacterStart(int _level,PlayerController _currentPlayerController)
    {
        CurrentPlayerController = _currentPlayerController;
        CharacterLevel = _level;
        GetComeFrom(_level);
        GetEntry(_level);//��ô���
        GetSalary(_level);//���ȷ�����ʺ͹�Ӷ��
        GetPerfectValue();//�������ֵ
        Entry.OriginalEffect(this);//�����������Ӱ������������
        RemarkVarible();
    }

    private void RemarkVarible()//��¼�����ɱ���ֵ
    {
        VaribleBackup = new List<float>();
        VaribleBackup.Add(StaminaDecreaseSpeed);//0
        VaribleBackup.Add(StaminaRecoverSpeed);//1
        VaribleBackup.Add(Salary);//2
        VaribleBackup.Add(PerfectValue);//3

    }
    public void InitializedVariable()//��ԭ���޸ĵ���ֵ
    {
        StaminaDecreaseSpeed = VaribleBackup[0];
        StaminaRecoverSpeed = VaribleBackup[1];
        Salary = VaribleBackup[2];
        PerfectValue = VaribleBackup[3];
        IsPlaying = false;
    }


    public void UpdateStaminaDecreaseSpeed(int _actAccount)//���ݾ籾Ļ���������������ٶ�
    {
        StaminaDecreaseSpeed *= 1 + _actAccount/10;
    }
    public void DecreaseStamita()//��������ֵ
    {
        if (CurrentStamina + StaminaDecreaseSpeed < 0)
        {
            
        }
        else
        {
            CurrentStamina += StaminaDecreaseSpeed;
        }
    }
    public void RecoverStamita()//�ָ�����ֵ
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
    public void ChangeName(string _newName)//������
    {
        Name = _newName;
    }
    private void GetComeFrom(int _level)
    {
        switch (_level)
        {
            case 0:
                ComeFrom = "����Ȧ�ڹ�";
                break;
            case 1:
                ComeFrom = "�ֱ�����";
                break;
            case 2:
                ComeFrom = "��Ӱ��Ϸ����";
                break;
            default:
                break;
        }
    }

    private void GetEntry(int _level)//�ӳ��������ȡ����
    {
        float _bottomRate;//�鵽��ͨ��������
        float _middleRate;//�嵽�߼���������
        float _topRate;//�鵽��˵��������
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
            _entryPool = Resources.Load<EntryPool>("Entry Pool1");//��ȡ��ͨ������Դ��
            EntryPool = "Entry Pool1";
        }
        else if (_bottomRate<_rate && _rate <= 1 - _topRate)
        {
            _entryPool = Resources.Load<EntryPool>("Entry Pool2");//��ȡ�߼�������Դ��
            EntryPool = "Entry Pool2";
        }
        else if (_bottomRate + _middleRate < _rate)
        {
            _entryPool = Resources.Load<EntryPool>("Entry Pool3");//��ȡ��˵������Դ��
            EntryPool = "Entry Pool3";
        }
        EntryMax = _entryPool.entries.Count;//��ȡ����������
        EntryNum =(int) Random.Range(0, EntryMax);//�����ȡ�������
        Entry = _entryPool.entries[EntryNum].GetComponent<IEntryInterface>() ;//���ݴ�����Ż�ȡ����
        EntryName = Entry.ReturnName();//��ô�����
        EntryImfo = Entry.ReturnImfo();//��ô�����׺
        EntrySprite = Entry.ReturnSprite();//��ô���ͼƬ
    } 

    public void GetSalary(int _level)//ȷ����н�͹�Ӷ��
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


    private void GetPerfectValue()//�����ȡ����ֵ
    {
        PerfectValue = Random.Range(30, 200);
    }
    public void AfterEffect()//�����������Ӱ����㣬������
    {
        Entry.AfterEffect(this);
    }
    public void PerfectEffect()//�������ʱ��Ӱ�죬������
    {
        Entry.PerfectEffect(this);
    }
    public void WeekEffect()//�����������Ӱ��ÿ�ܣ�������
    {
        Entry.WeekEffect(this);
    }


    public void LoadDrama(GameObject _drama)//���Ӿ籾
    {
        CurrentDrama = _drama.GetComponent<MainDrama>();
    }

    public void ReflashEntry()
    {
        EntryPool _entryPool = Resources.Load<EntryPool>(EntryPool);
        Entry = _entryPool.entries[EntryNum].GetComponent<IEntryInterface>();
    }
}
