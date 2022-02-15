using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entry08 : EntryVoid, IEntryInterface
{
    public string Name = "֤���Լ�";//������
    public string Imformation = "Ⱥ������ֵ����5%";//����˵��
    public int EntryLevel = 1;//�����ȼ�
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
        foreach (Character _item in _character.CurrentDrama.CharactersList)
        {
            if (_item == null) return;
            ChangePerfectValue(0.05f, _item);
        }
    }
    void IEntryInterface.PerfectEffect(Character _character)//����ʱ��Ӱ�죨�������ʡ�����������
    {

    }
    void IEntryInterface.WeekEffect(Character _character)//ÿһ��Ӱ�죨�ʲ�����˿�����ӷѣ�
    {

    }

}