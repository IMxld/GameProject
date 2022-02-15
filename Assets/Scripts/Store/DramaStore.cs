using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaStore : MonoBehaviour
{
    /// <summary>
    /// ���۾籾
    /// </summary>
    public List<GameObject> DramaSellList;
    public Dictionary<int, GameObject> DramaSellDict;
    public int CurrentBookNum;//��ǰ�����籾
    public PlayerController CurrentPlayerController;//��ǰ�浵
    public GameObject[] Sell;//����ť
    public GameObject[] IsSell;//�ѳ���
    public GameObject RemainText;//��ʾ
    private void Start()
    {
        CurrentPlayerController = MainController.mainController.CurrentPlayerController;
        DramaPool _dramaPool = Resources.Load<DramaPool>("Drama Pool");//��ȡ�籾��Դ
        DramaSellList = _dramaPool.Dramas;
        
        CheckIsSell();

    }

    public void DramaSelling()
    {
        GameObject _drama = DramaSellList[CurrentBookNum];
        if (CurrentPlayerController.Property >= _drama.GetComponent<MainDrama>().Price)//�ж��Ƿ���Ǯ����
        {
            MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/RecruitAndBuy"));
            CurrentPlayerController.Property -= _drama.GetComponent<MainDrama>().Price;//��Ǯ
            CurrentPlayerController.DramaLibrary.Add(_drama);//�浵��Ӿ籾
            CurrentPlayerController.DramaAdd++;
            CheckIsSell();
        }
        else
        {
            StartCoroutine(Remain());
        }
    }
    public void ChangeCurrentDramaNum(int _index)
    {
        CurrentBookNum = _index;
    }
    IEnumerator Remain()
    {
        RemainText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        RemainText.SetActive(false);
    }

    public void CheckIsSell()
    {
        if (CurrentPlayerController.DramaLibrary.Count != 0)
        {
            foreach (GameObject _item in CurrentPlayerController.DramaLibrary)
            {
                int i = _item.GetComponent<MainDrama>().Num;
                Sell[i].SetActive(false);
                IsSell[i].SetActive(true);
            }
        }

    }

}
