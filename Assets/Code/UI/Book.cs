using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Book : MonoBehaviour
{
    public SpriteAsset BookContentSprite;
    public Image BookContent;
    public GameObject Totem;
    int ContentNum = 5;
    // Start is called before the first frame update
    void Start()
    {
        ShowContent(ContentNum);
    }

    public void LeftDown()
    {
        ContentNum--;
        if (ContentNum < 0)
        {
            ContentNum = BookContentSprite.Sprites.Count - 1;
        }
        ShowContent(ContentNum);

    }
    public void RightDown()
    {   
        ContentNum++;
        if (ContentNum > BookContentSprite.Sprites.Count - 1)
        {
            ContentNum = 0;
        }
        ShowContent(ContentNum);
    }

    public void ShowContent(int _index)
    {
        Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/µã»÷ÊéICON"));
        BookContent.sprite = BookContentSprite.Sprites[_index];
        if (_index == 5)
        {
            Totem.SetActive(true);
        }
        else
        {
            Totem.SetActive(false);
        }
    }
}
