using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntryInterface
{

    public void OriginalEffect(Character _character);//终身影响(自身薪水、自身完美值、自身疲劳损耗速度、自身疲劳恢复速度...)
    public void AfterEffect(Character _character);//每一部剧结算时影响（收入、增长粉丝数）
    public void PerfectEffect(Character _character);//完美时影响
    public void WeekEffect(Character _character);//每一周影响（资产、粉丝量、杂费）
    public string ReturnName();//返回名字
    public string ReturnImfo();//返回信息

    public Sprite ReturnSprite();//返回图片
}
