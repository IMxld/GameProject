using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry19 : EntryVoid, IEntryInterface
{
    public string Name = "����������";//������
    public string Imformation = "��������ʱ���������ƣ��ֵ";//����˵��
    public int EntryLevel = 3;//�����ȼ�
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

    }

    void IEntryInterface.PerfectEffect(Character _character)//����ʱ��Ӱ�죨�������ʡ�����������
    {
        _character.StaminaDecreaseSpeed = 0;
    }

    void IEntryInterface.WeekEffect(Character _character)//ÿһ��Ӱ�죨�ʲ���˿�����ӷѣ�
    {

    }

}
