using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class MainController : MonoBehaviour
{
    public PlayerFile playerFilePool;//存档库
    public PlayerController CurrentPlayerController;//当前存档
    public int ReadFileNum;//读档编号
    private static MainController _mainController;//实例化
    //开演
    public GameObject CurrentDrama;//当前操作剧本
    public List<GameObject> PreDramas;//待演剧本列
    public int CurrentDramaNum = 0;//当前操作列位
    //设置
    public float TotalVoice = 100;
    public float BGM = 100;
    public float EffectVoice = 100;
    /// <summary>
    /// 幕布
    /// </summary>
    //周结算
    /// <summary>
    /// 这周增加的粉丝量
    /// </summary>
    public float IncreaseFans;
    /// <summary>
    /// 这周增加的金钱
    /// </summary>
    public float IncreaseProperty;
    /// <summary>
    /// 这周的观众
    /// </summary>
    public float Audience;
    public bool IsSelectedBoard;
    /// <summary>
    /// 音乐播放器
    /// </summary>
    public AudioSource[] audioSources;
    public static MainController mainController
    {
        get
        {
            if (_mainController == null)
            {
                _mainController = Transform.FindObjectOfType<MainController>();
                return _mainController;
            }
            else
            {
                return _mainController;
            }
        }
    }

    private void Start()
    {
        playerFilePool = Resources.Load<PlayerFile>("Player File");
        PlayingVoice(0, Resources.Load<AudioClip>("Voice/BGM1"));//背景音乐
        DontDestroyOnLoad(gameObject);
        NewFile();
        StartCoroutine(ChangeScene());

    }

    private void Update()
    {
        ChangeVolume();
    }
    /// <summary>
    /// 开始游戏前加载组件以及介绍
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeScene()
    {
        //Curtain.curtain.CurtainClose();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FirstPage");
        //Curtain.curtain.CurtainOpen();
    }
    
    /// <summary>
    /// 根据编号读档
    /// </summary>
    /// <param name="_readFileNum"></param>
    public void ReadFile(int _readFileNum)
    {
        if (playerFilePool.playerControllers[_readFileNum].CharactersLibrary.Count == 0)
        {
            //空
        }
        else
        {
            FileTranslate(playerFilePool.playerControllers[_readFileNum], CurrentPlayerController);
            ToScene.toScene.GoTo("BeginPage");
        }
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ContinueFile()
    {
        FileTranslate(playerFilePool.playerControllers[0], CurrentPlayerController);
    }

    /// <summary>
    /// 根据编号存档
    /// </summary>
    /// <param name="_loadFileNum"></param>
    public void LoadFile(int _loadFileNum)
    {
        CurrentPlayerController.GetFileTime();
        FileTranslate(CurrentPlayerController, playerFilePool.playerControllers[_loadFileNum]);
    }

    /// <summary>
    /// 开始新档
    /// </summary>
    public void NewFile()
    {
        PlayerController newPlayerFlie = new PlayerController();//新实例化一个存档
        CurrentPlayerController = newPlayerFlie;//当前档变成新的档

    }
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        CurrentPlayerController.StartNewGame();
    }


    /// <summary>
    /// 把存档值赋给目标存档
    /// </summary>
    /// <param name="_mainFile"></param>
    /// <param name="_targetFlie"></param>
    public void FileTranslate(PlayerController _mainFile, PlayerController _targetFlie)
    {
        _targetFlie.Property = _mainFile.Property;
        _targetFlie.FansCount = _mainFile.FansCount;
        _targetFlie.WeekTime = _mainFile.WeekTime;
        _targetFlie.CharactersLibrary = _mainFile.CharactersLibrary;
        _targetFlie.DramaLibrary = _mainFile.DramaLibrary;
        _targetFlie.FileTime = _mainFile.FileTime;
        _targetFlie.RecruitList = _mainFile.RecruitList;
        _targetFlie.RecruitCharacters = _mainFile.RecruitCharacters;
    }
    /// <summary>
    /// 更新招聘列
    /// </summary>
    public void UpdateRecruitList()
    {
        CharactersPool _charactersPool = Resources.Load<CharactersPool>("Characters Pool");
        int i = 0;
        foreach (bool _item in CurrentPlayerController.RecruitList)
        {
            if (_item)
            {
                Character _son = new Character();
                Character _mom = _charactersPool.Characters[Random.Range(0, _charactersPool.Characters.Count)];//根据演员库随机生成演员
                ActorTranlate(_mom, _son);
                _son.CharacterStart(i, CurrentPlayerController);
                CurrentPlayerController.RecruitCharacters[i] = _son;
            }
            else
            {
                CurrentPlayerController.RecruitCharacters[i] = new Character();
            }
            i++;
        }

    }
    /// <summary>
    /// 初始化剧本
    /// </summary>
    public void InitializePreDramas()
    {
        for (int i = 0; i < PreDramas.Count; i++)
        {
            if (PreDramas[i])
            {
                PreDramas[i].GetComponent<MainDrama>().Initialize();
                PreDramas[i] = null;
            }
        }
        IncreaseProperty = 0;
        IncreaseFans = 0;
        Audience = 0;
    }
    /// <summary>
    /// 初始化当前剧本的演员
    /// </summary>
    public void InitializeCurrentDramaCharacter()
    {
        foreach (Character _item in CurrentDrama.GetComponent<MainDrama>().CharactersList)
        {
            if (_item != null)
            {
                _item.InitializedVariable();
            }
        }
    }

    /// <summary>
    /// 加载剧本
    /// </summary>
    public void LoadDrama()
    {
        PreDramas[CurrentDramaNum] = CurrentDrama;

    }

    /// <summary>
    /// 开始演出
    /// </summary>
    public void Playing()
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Playing"));
        CurrentPlayerController.LastWeekPlayNum = 0;//记录上周演出次数
        GameObject _item = null;
        for (int i = 0; i < PreDramas.Count; i++)
        {
            _item = PreDramas[i];
            if (_item != null)
            {
                _item.GetComponent<MainDrama>().Statement();
                CurrentPlayerController.LastWeekPlayNum++;
            }
        }
        UpdateRecruitList();
        CurrentPlayerController.WeekGone();
    }
    private void ChangeVolume()
    {
        float _totalVoice = TotalVoice / 100;
        float _bgm = BGM / 100;
        float _effectVoice = EffectVoice / 100;
        AudioSource _audioSourceBGM = gameObject.GetComponents<AudioSource>()[0];
        _audioSourceBGM.loop = true;
        _audioSourceBGM.volume = _bgm * _totalVoice;
        AudioSource _audioSourceEffect = gameObject.GetComponents<AudioSource>()[1];
        _audioSourceEffect.loop = false;
        _audioSourceEffect.volume = _totalVoice * _effectVoice; ;

    }
    /// <summary>
    /// 播放BGM和音效
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_audioClip"></param>
    public void PlayingVoice(int _index, AudioClip _audioClip)
    {
        AudioSource _audioSource = gameObject.GetComponents<AudioSource>()[_index];
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
    /// <summary>
    /// 母子
    /// </summary>
    /// <param name="mom"></param>
    /// <param name="son"></param>
    public void ActorTranlate(Character mom,Character son)
    {
        son.Name = mom.Name;
        son.Degree = mom.Degree;
        son.Age = mom.Age;
        son.Background = mom.Background;
        son.CharacterChooseSprite = mom.CharacterChooseSprite;
        son.CharacterSprite = mom.CharacterSprite;
    }
}
