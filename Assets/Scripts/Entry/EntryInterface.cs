using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntryInterface
{

    public void OriginalEffect(Character _character);//����Ӱ��(����нˮ����������ֵ������ƣ������ٶȡ�����ƣ�ͻָ��ٶ�...)
    public void AfterEffect(Character _character);//ÿһ�������ʱӰ�죨���롢������˿����
    public void PerfectEffect(Character _character);//����ʱӰ��
    public void WeekEffect(Character _character);//ÿһ��Ӱ�죨�ʲ�����˿�����ӷѣ�
    public string ReturnName();//��������
    public string ReturnImfo();//������Ϣ

    public Sprite ReturnSprite();//����ͼƬ
}
