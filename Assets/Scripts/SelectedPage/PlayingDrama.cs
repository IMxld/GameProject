using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayingDrama : MonoBehaviour
{
    /// <summary>
    /// 剧本框列
    /// </summary>
    public List<Image> FrameImageList; 
    /// <summary>
    /// 剧本选择面板实例
    /// </summary>
    private static PlayingDrama _playingDrama;
    public static PlayingDrama playingDrama
    {
        get
        {
            if (_playingDrama == null)
            {
                _playingDrama = Transform.FindObjectOfType<PlayingDrama>();
                return _playingDrama;
            }
            else
            {
                return _playingDrama;
            }
        }
    }
    /// <summary>
    /// 剧本放置框
    /// </summary>
    public PlayerController CurrentPlayerController;//当前存档
    /// <summary>
    /// 选择剧本界面
    /// </summary>
    public GameObject SelectBoard;
    /// <summary>
    /// 当前选择的空格
    /// </summary>
    /// <summary>
    /// 待放剧本框
    /// </summary>
    public Sprite OriginalFrameSprite;
    public GameObject RemainText;
    // Start is called before the first frame update
    void Start()
    {
        ChangeFrameSprite();
        CurrentPlayerController = MainController.mainController.CurrentPlayerController;
        ChangeSelectBoardCondition(MainController.mainController.IsSelectedBoard);
    }
    /// <summary>
    /// 将选中剧本的海报图填入空框内
    /// </summary>
    private void ChangeFrameSprite()
    {
        for (int i = 0; i < FrameImageList.Count; i++)
        {
            GameObject _gameobject = MainController.mainController.PreDramas[i];
            if (_gameobject != null)
            {
                FrameImageList[i].sprite = _gameobject.GetComponent<MainDrama>().ImageBackground;
            }
            else
            {
                FrameImageList[i].sprite = OriginalFrameSprite;
            }
        }
    }
    /// <summary>
    /// 开始演出
    /// </summary>
    public void Playing()
    {
        bool _canPlay = false;
        foreach (GameObject _gameObject in MainController.mainController.PreDramas)//判断能否演出
        {
            if (_gameObject != null)
            {
                _canPlay = true;
            }
        }

        if (_canPlay)
        {
            MainController.mainController.Playing();
            ToScene.toScene.GoTo("PlayingPage");
        }
        else
        {
            StartCoroutine(Remain());
        }

    }
    /// <summary>
    /// 实时改变当前选择的空格
    /// </summary>
    /// <param name="_index"></param>
    public void ChangeNum(int _index)
    {
        MainController.mainController.CurrentDramaNum = _index;
    }
    /// <summary>
    /// 显示/取消显示剧本选择板
    /// </summary>
    /// <param name="_index"></param>
    public void ChangeSelectBoardCondition(bool _index)
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Back"));
        SelectBoard.SetActive(_index);
    }

    /// <summary>
    /// 调用剧本
    /// </summary>
    /// <param name="_index"></param>
    public void LoadDrama(int _index)
    {
        GameObject _drama = Resources.Load<DramaPool>("DramaLibrary/Frame/Drama Pool" + MainController.mainController.CurrentDramaNum).Dramas[_index];
        _drama.GetComponent<MainDrama>().DramaStart(CurrentPlayerController);
        _drama.GetComponent<MainDrama>().CharactersList = new Character[_drama.GetComponent<MainDrama>().CharactersCount];
        MainController.mainController.CurrentDrama = _drama;
        SceneManager.LoadScene("ActorPage");
    }

    IEnumerator Remain()
    {
        RemainText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        RemainText.SetActive(false);
    }
}
