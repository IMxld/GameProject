using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry10 : EntryVoid, IEntryInterface
{
    public string Name = "ͬ٭ѹ��";//������
    public string Imformation = "�����ݳ�����5%�ĸ��ʷ�������";//����˵��
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

    }
    void IEntryInterface.PerfectEffect(Character _character)//����ʱ��Ӱ�죨�������ʡ�����������
    {

    }
    void IEntryInterface.WeekEffect(Character _character)//ÿһ��Ӱ�죨�ʲ�����˿�����ӷѣ�
    {
        foreach (Character _item in _character.CurrentDrama.CharactersList)
        {
            float _random = Random.Range(0, 1);
            if (_random < 0.05f)//������˾ͼӹ���
            {
                _character.CurrentPlayerController.Property += (int)_item.Salary;
            }
        }
    }



}
