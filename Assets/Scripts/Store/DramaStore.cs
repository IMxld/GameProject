using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaStore : MonoBehaviour
{
    /// <summary>
    /// 代售剧本
    /// </summary>
    public List<GameObject> DramaSellList;
    public Dictionary<int, GameObject> DramaSellDict;
    public int CurrentBookNum;//当前操作剧本
    public PlayerController CurrentPlayerController;//当前存档
    public GameObject[] Sell;//购买按钮
    public GameObject[] IsSell;//已出售
    public GameObject RemainText;//提示
    private void Start()
    {
        CurrentPlayerController = MainController.mainController.CurrentPlayerController;
        DramaPool _dramaPool = Resources.Load<DramaPool>("Drama Pool");//获取剧本资源
        DramaSellList = _dramaPool.Dramas;
        
        CheckIsSell();

    }

    public void DramaSelling()
    {
        GameObject _drama = DramaSellList[CurrentBookNum];
        if (CurrentPlayerController.Property >= _drama.GetComponent<MainDrama>().Price)//判断是否有钱购买
        {
            MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/RecruitAndBuy"));
            CurrentPlayerController.Property -= _drama.GetComponent<MainDrama>().Price;//扣钱
            CurrentPlayerController.DramaLibrary.Add(_drama);//存档添加剧本
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
