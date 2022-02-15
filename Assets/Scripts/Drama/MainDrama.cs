using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDrama : MonoBehaviour
{
    public string Name;//����
    public int Num;//�籾���
    [TextArea]
    public TextAsset textAsset;//�����ű�
    public string Plot;//�������
    public TextAsset Dialog;//���¶Ի�
    public int ActCount;//Ļ��
    public int CharactersCount;//��ɫ��
    public Character[] CharactersList;//��ɫ��
    public string[] CharactersNameList;//��ɫ����
    /// <summary>
    /// ������
    /// </summary>
    public int Audience;
    /// <summary>
    /// �籾����ֵ
    /// </summary>
    public int BasicCount;
    public int Price;//�ۼ�
    public int TicketPrice;//�籾Ʊ��
    public string Type;//�籾����
    /// <summary>
    /// �籾���
    /// </summary>
    public Sprite ImagePreference;
    public Sprite ImageBeSelected;
    /// <summary>
    /// �籾����
    /// </summary>
    public Sprite ImageBackground;
    /// <summary>
    /// �籾��������
    /// </summary>
    public float BasicIncome = 0;
    /// <summary>
    /// �籾������˿������
    /// </summary>
    public float BasicFansIncrease = 0;
    /// <summary>
    /// �籾ʵ������
    /// </summary>
    public float DramaIncome = 0;
    /// <summary>
    /// �籾ʵ��������˿��
    /// </summary>
    public float FansIncrease = 0;
    /// <summary>
    /// ������
    /// </summary>
    public bool Perfect = false;
    /// <summary>
    /// ��������
    /// </summary>
    public float PerfectChance;
    /// <summary>
    /// �������ʤη�˿
    /// </summary>
    public float PerfectFansRate = 0.1f;
    /// <summary>
    /// �������ʤ�����
    /// </summary>
    public float PerfectIncomesRate = 0.1f;
    /// <summary>
    /// ��ȡ��ǰ�浵
    /// </summary>
    public PlayerController CurrentPlayerController;
    public void DramaStart(PlayerController _currentPlayerController)
    {
        CurrentPlayerController = _currentPlayerController;
    }

    public void Initialize()//��ʼ������
    {
        Audience = 0;
        DramaIncome = 0;
        FansIncrease = 0;
        BasicFansIncrease = 0;
        BasicIncome = 0;
        PerfectFansRate = 0.1f;
        PerfectIncomesRate = 0.1f;
        PerfectChance = 0;
        Perfect = false;
        for (int i = 0; i < CharactersList.Length; i++)//��ԭ
        {
            Character _item = CharactersList[i];
            _item.InitializedVariable();
        }
        for (int i = 0; i < CharactersList.Length; i++)
        {
            CharactersList[i] = null;
        }
        
    }
    /// <summary>
    /// �����������
    /// </summary>
    private void CaculateAudience()
    {
        Audience =(int)(BasicCount + Random.Range(0.5f, 0.8f) * CurrentPlayerController.FansCount);//����ֵ+��50%��80%��˿����������ȡ����
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void CalculateIncome()
    {
        BasicIncome = DramaIncome = TicketPrice * Audience;//������*��Ʊ
    }
    /// <summary>
    /// �����˿��
    /// </summary>
    public void CalculateFans()
    {
        BasicFansIncrease = FansIncrease = BasicCount + Mathf.Log(Mathf.Pow(Audience, 2), Mathf.Exp(1));//����ֵ+ln(x^2) + ��һ����˿��
    }
    public void IsPerfect()
    {
        float _random = Random.Range(0f, 1f);
        if (_random < PerfectChance)//������0~��������֮�䣬����������������
        {
            Perfect = true;
        }
        else
        {
            Perfect = false;
        }
    }

    public void Statement()//�ݳ�����
    {
        float _totalPerfect = 0;
        CaculateAudience();//���������
        CalculateIncome();//�����������
        CalculateFans();//���������˿������

        //�������㼼��
        foreach (Character _item in CharactersList)
        {
            if (_item != null)
            {
                _item.ReflashEntry() ;
                _item.AfterEffect();
            }
        }

        //����
        foreach (Character _item in CharactersList)//��Ա����ֵ���
        {
            if (_item != null)
            {
                _totalPerfect += _item.PerfectValue;
            }
        }
        PerfectChance += 0.1f + ((_totalPerfect / CharactersList.Length - 50) / 100) * 0.2f;//��������
        IsPerfect();//������ײ����
        if (Perfect)//�������
        {
            foreach (Character _item in CharactersList)//������������
            {
                if (_item != null)
                {
                    _item.PerfectEffect();

                }
            }
            DramaIncome *= 1 + PerfectFansRate;//��������++
            FansIncrease *= 1 + PerfectIncomesRate;//������˿������++
        }

        //���ս���
        CurrentPlayerController.Property += (int) DramaIncome;//�ʲ�����
        CurrentPlayerController.FansCount += (int) FansIncrease;//��˿������
        MainController.mainController.CurrentPlayerController.LastWeekIncome += (int)DramaIncome;//��������
        MainController.mainController.CurrentPlayerController.LastWeekFans += (int)FansIncrease;//��������
        MainController.mainController.CurrentPlayerController.LastWeekAudience += (int)this.Audience;//���ܹ���

        //��������

        foreach (Character _item in CurrentPlayerController.CharactersLibrary)//��û���ݳ���ָ��������������ݳ���۳�����
        {
            if (_item.IsPlaying)
            {
                _item.DecreaseStamita();
            }
            else
            {
                _item.RecoverStamita();
            }
        }
    }
}
