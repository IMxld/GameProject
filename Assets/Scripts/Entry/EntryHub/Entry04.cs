using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry04 : EntryVoid, IEntryInterface
{
    public string Name = "领导力";//词条名
    public string Imformation = "所有演员损耗的疲劳值降低5%";//词条说明
    public int EntryLevel = 1;//词条等级
    public Sprite EntrySprite;//图片
    Sprite IEntryInterface.ReturnSprite()//返回图片
    {
        return EntrySprite;
    }
    string IEntryInterface.ReturnName()//返回名
    {
        return Name;
    }
    string IEntryInterface.ReturnImfo()//返回信息
    {
        return Imformation;
    }
    void IEntryInterface.OriginalEffect(Character _character)//终身影响(自身薪水、自身完美值、自身疲劳损耗速度、自身疲劳恢复速度...)
    {

    }

    void IEntryInterface.AfterEffect(Character _character)//每一部剧结算时影响（收入、增长粉丝数）
    {
        foreach (Character _item in _character.CurrentDrama.CharactersList)
        {
            ChangeDecreaseStaminaSpeed(-0.05f, _item);
        }
    }
    void IEntryInterface.PerfectEffect(Character _character)//完美时有影响（完美倍率、完美触发）
    {

    }

    void IEntryInterface.WeekEffect(Character _character)//每一周影响（资产、粉丝量、杂费）
    {

    }

}
