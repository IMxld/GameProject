using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayingDrama : MonoBehaviour
{
    /// <summary>
    /// �籾����
    /// </summary>
    public List<Image> FrameImageList; 
    /// <summary>
    /// �籾ѡ�����ʵ��
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
    /// �籾���ÿ�
    /// </summary>
    public PlayerController CurrentPlayerController;//��ǰ�浵
    /// <summary>
    /// ѡ��籾����
    /// </summary>
    public GameObject SelectBoard;
    /// <summary>
    /// ��ǰѡ��Ŀո�
    /// </summary>
    /// <summary>
    /// ���ž籾��
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
    /// ��ѡ�о籾�ĺ���ͼ����տ���
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
    /// ��ʼ�ݳ�
    /// </summary>
    public void Playing()
    {
        bool _canPlay = false;
        foreach (GameObject _gameObject in MainController.mainController.PreDramas)//�ж��ܷ��ݳ�
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
    /// ʵʱ�ı䵱ǰѡ��Ŀո�
    /// </summary>
    /// <param name="_index"></param>
    public void ChangeNum(int _index)
    {
        MainController.mainController.CurrentDramaNum = _index;
    }
    /// <summary>
    /// ��ʾ/ȡ����ʾ�籾ѡ���
    /// </summary>
    /// <param name="_index"></param>
    public void ChangeSelectBoardCondition(bool _index)
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Back"));
        SelectBoard.SetActive(_index);
    }

    /// <summary>
    /// ���þ籾
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
