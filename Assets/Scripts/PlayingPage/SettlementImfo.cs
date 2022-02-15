using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettlementImfo : MonoBehaviour
{
    public GameObject[] DramasBorad;
    public Sprite[] PerfectSprites;
    public List<GameObject> PlayingDramas;
    // Start is called before the first frame update
    void Start()
    {
        PlayingDramas = MainController.mainController.PreDramas;//获得在演剧本列

        for (int i = 0; i < DramasBorad.Length; i++)
        {
            if (PlayingDramas[i])
            {
                ShowImfo(DramasBorad[i],PlayingDramas[i]);
            }
        }
        MainController.mainController.InitializePreDramas();//结算后初始化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowImfo(GameObject _gameObject,GameObject _drama)
    {
        _gameObject.SetActive(true);
        MainDrama _mainDrama = _drama.GetComponent<MainDrama>();
        _gameObject.transform.GetChild(0).GetComponent<Text>().text = "《 " + _mainDrama.Name + " 》";//剧名
        _gameObject.transform.GetChild(1).GetComponent<Text>().text = ShowActorName(_mainDrama);//主演
        _gameObject.transform.GetChild(2).GetComponent<Text>().text ="财富：" + " + " + (int)_mainDrama.DramaIncome;//收入
        _gameObject.transform.GetChild(3).GetComponent<Text>().text = "×" + string.Format("{0:F1}", _mainDrama.DramaIncome/_mainDrama.BasicIncome);//收入倍率
        _gameObject.transform.GetChild(4).GetComponent<Text>().text = "粉丝数：" + " + " + (int)_mainDrama.FansIncrease;//粉丝数
        _gameObject.transform.GetChild(5).GetComponent<Text>().text = "×" + string.Format("{0:F1}",_mainDrama.FansIncrease/_mainDrama.BasicFansIncrease);//粉丝倍率
        _gameObject.transform.GetChild(6).GetComponent<Image>().sprite = ShowPerfect(_mainDrama.Perfect);
    }

    private string ShowActorName(MainDrama _drama)
    {
        string _names = "主演：";
        foreach (Character _item in _drama.CharactersList)
        {
            _names += _item.Name + "  ";
        }
        return _names;
    }
    private Sprite ShowPerfect(bool _isPerfect)
    {
        if (_isPerfect)
        {
            return PerfectSprites[2];
        }
        else
        {
            return PerfectSprites[Random.Range(0, 2)];
        }
    }
}
