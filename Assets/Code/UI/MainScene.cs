using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainScene : MonoBehaviour
{
    public static MainScene mainScene;
    public static MainScene instance
    {
        get
        {
            if (mainScene != null)
            {
                return mainScene;
            }
            else
            {
                mainScene = Transform.FindObjectOfType<MainScene>();
                return mainScene;
            }
        }
    }
    public GameObject PlayerPoint;
    //sky
    public SpriteAsset BossStarSprits;
    public SpriteAsset RandomStarSprites;
    /// <summary>
    /// 星图大格子
    /// </summary>
    public List<Image> ConstellationImages;
    //totem
    public SpriteAsset TotemSprites;
    /// <summary>
    /// 书本小格子
    /// </summary>
    public List<Image> TotemsFrame;
    /// <summary>
    /// 当前页码
    /// </summary>
    public int CurrentBookContentNum = 0;
    /// <summary>
    /// 地
    /// </summary>
    public GameObject MapUI;
    /// <summary>
    /// 天
    /// </summary>
    public GameObject SkyUI;

    bool IsFree = true;
    // Start is called before the first frame update
    void Start()
    {
        Music.PlayingMusic(0, Resources.Load<AudioClip>("Audio/开始音乐2"));
        MapUI.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f);
        BossStarSprits = Resources.Load<SpriteAsset>("ScriptsObject/BossStarSprite");
        RandomStarSprites = Resources.Load<SpriteAsset>("ScriptsObject/RandomStarSprite");


    }
    // Update is called once per frame
    void Update()
    {
        MouseRoll();
    }

    /// <summary>
    /// 滚轮切换
    /// </summary>
    void MouseRoll()
    {
        float dv = Input.GetAxis("Mouse ScrollWheel");
        if (dv < 0)
        {
            CurrentBookContentNum++;
        }
        else if (dv > 0)
        {
            CurrentBookContentNum--;
        }
        CurrentBookContentNum = Mathf.Clamp(CurrentBookContentNum, 0, 15);
        if (CurrentBookContentNum <= 5)
        {

            if (MapUI.activeSelf)
            {
                foreach (Image item in MapUI.GetComponentsInChildren<Image>())
                {
                    item.CrossFadeAlpha(0, 0.5f, true);
                }
                StartCoroutine(ScaleChange(MapUI, new Vector3(1.5f, 1.5f, 1.5f)));
                StartCoroutine(WaitingThenSetActive(1, MapUI, false));
                PlayerPoint.GetComponent<LoadMark>().DropLines();

            }
        }
        else if (CurrentBookContentNum <= 10)
        {
            if (!MapUI.activeSelf)
            {
                MapUI.SetActive(true);
                foreach (Image item in MapUI.GetComponentsInChildren<Image>())
                {
                    item.CrossFadeAlpha(0, 0f, true);
                    item.CrossFadeAlpha(1, 0.5f, true);
                }
                StartCoroutine(ScaleChange(MapUI, new Vector3(1f, 1f, 1f)));
                PlayerPoint.GetComponent<LoadMark>().LoadLines();

            }
            if (SkyUI.activeSelf)
            {
                SkyUI.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
                StartCoroutine(WaitingThenSetActive(1, SkyUI, false));
            }
        }
        else
        {
            if (!SkyUI.activeSelf)
            {
                SkyUI.SetActive(true);
                SkyUI.GetComponent<Image>().CrossFadeAlpha(0, 0f, true);
                SkyUI.GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
            }
        }
    }

    IEnumerator ScaleChange(GameObject _item, Vector3 _target)
    {
        while (Mathf.Abs((_item.GetComponent<RectTransform>().localScale.x - _target.x)) > 0.1)
        {
            _item.GetComponent<RectTransform>().localScale = Vector3.Lerp(_item.GetComponent<RectTransform>().localScale, _target, Time.deltaTime);
            yield return null;
        }
        StopCoroutine("ScaleChange");
    }

    IEnumerator WaitingThenSetActive(int _waitTime, GameObject _gameObject, bool _isActive)
    {
        yield return new WaitForSeconds(_waitTime);
        _gameObject.SetActive(_isActive);
    }

    public void ChangeTotem(int _bossNum)
    {
        TotemsFrame[_bossNum].sprite = TotemSprites.Sprites[_bossNum];
    }
    public void RandomTotem(int _bossNum)
    {
        ConstellationImages[_bossNum].sprite = RandomStarSprites.Sprites[UnityEngine.Random.Range(0, RandomStarSprites.Sprites.Count)];
    }
    /// <summary>
    /// 加载星图
    /// </summary>
    /// <param name="_bossNum">Boss编号</param>
    /// <param name="_targetFrame">格子编号</param>
    public void LoadStar(int _bossNum, int _targetFrame)
    {
        try
        {
            ConstellationImages[_targetFrame].sprite = BossStarSprits.Sprites[_bossNum];

        }
        catch (NullReferenceException) { }
    }

}
