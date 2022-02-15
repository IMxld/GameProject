using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDrama : MonoBehaviour
{
    public string Name;//名字
    public int Num;//剧本编号
    [TextArea]
    public TextAsset textAsset;//动画脚本
    public string Plot;//故事情节
    public TextAsset Dialog;//故事对话
    public int ActCount;//幕数
    public int CharactersCount;//角色数
    public Character[] CharactersList;//角色列
    public string[] CharactersNameList;//角色名列
    /// <summary>
    /// 观众数
    /// </summary>
    public int Audience;
    /// <summary>
    /// 剧本基础值
    /// </summary>
    public int BasicCount;
    public int Price;//售价
    public int TicketPrice;//剧本票价
    public string Type;//剧本类型
    /// <summary>
    /// 剧本外观
    /// </summary>
    public Sprite ImagePreference;
    public Sprite ImageBeSelected;
    /// <summary>
    /// 剧本背景
    /// </summary>
    public Sprite ImageBackground;
    /// <summary>
    /// 剧本基础收入
    /// </summary>
    public float BasicIncome = 0;
    /// <summary>
    /// 剧本基础粉丝增长量
    /// </summary>
    public float BasicFansIncrease = 0;
    /// <summary>
    /// 剧本实际收入
    /// </summary>
    public float DramaIncome = 0;
    /// <summary>
    /// 剧本实际增长粉丝数
    /// </summary>
    public float FansIncrease = 0;
    /// <summary>
    /// 完美吗
    /// </summary>
    public bool Perfect = false;
    /// <summary>
    /// 完美几率
    /// </summary>
    public float PerfectChance;
    /// <summary>
    /// 完美倍率の粉丝
    /// </summary>
    public float PerfectFansRate = 0.1f;
    /// <summary>
    /// 完美倍率の收入
    /// </summary>
    public float PerfectIncomesRate = 0.1f;
    /// <summary>
    /// 获取当前存档
    /// </summary>
    public PlayerController CurrentPlayerController;
    public void DramaStart(PlayerController _currentPlayerController)
    {
        CurrentPlayerController = _currentPlayerController;
    }

    public void Initialize()//初始化变量
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
        for (int i = 0; i < CharactersList.Length; i++)//还原
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
    /// 计算观众数量
    /// </summary>
    private void CaculateAudience()
    {
        Audience =(int)(BasicCount + Random.Range(0.5f, 0.8f) * CurrentPlayerController.FansCount);//基础值+（50%～80%粉丝数四舍五入取整）
    }
    /// <summary>
    /// 计算收入
    /// </summary>
    public void CalculateIncome()
    {
        BasicIncome = DramaIncome = TicketPrice * Audience;//观众数*门票
    }
    /// <summary>
    /// 计算粉丝量
    /// </summary>
    public void CalculateFans()
    {
        BasicFansIncrease = FansIncrease = BasicCount + Mathf.Log(Mathf.Pow(Audience, 2), Mathf.Exp(1));//基础值+ln(x^2) + 上一个粉丝数
    }
    public void IsPerfect()
    {
        float _random = Random.Range(0f, 1f);
        if (_random < PerfectChance)//若是在0~完美几率之间，则完美，否则不完美
        {
            Perfect = true;
        }
        else
        {
            Perfect = false;
        }
    }

    public void Statement()//演出结算
    {
        float _totalPerfect = 0;
        CaculateAudience();//计算观众数
        CalculateIncome();//计算基础收入
        CalculateFans();//计算基础粉丝增长量

        //发动结算技能
        foreach (Character _item in CharactersList)
        {
            if (_item != null)
            {
                _item.ReflashEntry() ;
                _item.AfterEffect();
            }
        }

        //完美
        foreach (Character _item in CharactersList)//演员完美值相加
        {
            if (_item != null)
            {
                _totalPerfect += _item.PerfectValue;
            }
        }
        PerfectChance += 0.1f + ((_totalPerfect / CharactersList.Length - 50) / 100) * 0.2f;//完美几率
        IsPerfect();//完美碰撞！！
        if (Perfect)//如果完美
        {
            foreach (Character _item in CharactersList)//发动完美技能
            {
                if (_item != null)
                {
                    _item.PerfectEffect();

                }
            }
            DramaIncome *= 1 + PerfectFansRate;//完美收入++
            FansIncrease *= 1 + PerfectIncomesRate;//完美粉丝增长量++
        }

        //最终结算
        CurrentPlayerController.Property += (int) DramaIncome;//资产增加
        CurrentPlayerController.FansCount += (int) FansIncrease;//粉丝量增加
        MainController.mainController.CurrentPlayerController.LastWeekIncome += (int)DramaIncome;//这周收入
        MainController.mainController.CurrentPlayerController.LastWeekFans += (int)FansIncrease;//这周增粉
        MainController.mainController.CurrentPlayerController.LastWeekAudience += (int)this.Audience;//这周观众

        //计算体力

        foreach (Character _item in CurrentPlayerController.CharactersLibrary)//若没有演出则恢复体力，若参与演出则扣除体力
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
