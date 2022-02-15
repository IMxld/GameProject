using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAndLoad : MonoBehaviour
{
    public GameObject[] LoadFrameImfo;
    public Sprite FileSprite;
    void Start()
    {
        ChangeFrameForm();
    }


    public void ChangeFrameForm()
    {
        int i = 0;
        PlayerFile _playerFilePool = Resources.Load<PlayerFile>("Player File");
        foreach (PlayerController _item in _playerFilePool.playerControllers)
        {
            if (_item.CharactersLibrary.Count != 0)
            {
                LoadFrameImfo[i].SetActive(true);
                LoadFrameImfo[i].GetComponentInParent<Image>().sprite = FileSprite;
                LoadFrameImfo[i].transform.GetChild(0).GetComponent<Text>().text = _item.Property + "  £¤";
                LoadFrameImfo[i].transform.GetChild(1).GetComponent<Text>().text = _item.FansCount + "  Î»";
                LoadFrameImfo[i].transform.GetChild(2).GetComponent<Text>().text = _item.WeekTime + "  ÖÜ";
                LoadFrameImfo[i].transform.GetChild(3).GetComponent<Text>().text = _item.CharactersLibrary.Count + "  ¸ö";
                LoadFrameImfo[i].transform.GetChild(4).GetComponent<Text>().text = _item.FileTime;

            }
            i++;
        }
    }
}
