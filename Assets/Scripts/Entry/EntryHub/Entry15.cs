using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry15 : EntryVoid, IEntryInterface
{
    public string Name = "���ӿ��";//������
    public string Imformation = "�������ּ�������10%";//����˵��
    public int EntryLevel = 2;//�����ȼ�
    public Sprite EntrySprite;//ͼƬ
    Sprite IEntryInterface.ReturnSprite()//����ͼƬ
    {
        return EntrySprite;
    }
    string IEntryInterface.ReturnName()//������
    {
        return Name;
    }
    string IEntryInterface.ReturnImfo()//������Ϣ
    {
        return Imformation;
    }

    void IEntryInterface.OriginalEffect(Character _character)//����Ӱ��(����нˮ����������ֵ������ƣ������ٶȡ�����ƣ�ͻָ��ٶ�...)
    {

    }

    void IEntryInterface.AfterEffect(Character _character)//ÿһ�������ʱӰ�죨���롢������˿����������
    {
        ChangePecfectChance(0.1f, _character.CurrentDrama);
    }

    void IEntryInterface.PerfectEffect(Character _character)//����ʱ��Ӱ�죨�������ʡ�����������
    {

    }

    void IEntryInterface.WeekEffect(Character _character)//ÿһ��Ӱ�죨�ʲ�����˿�����ӷѣ�
    {

    }

}
