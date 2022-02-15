using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    private static ToScene _toScene;
    public static ToScene toScene
    {
        get
        {
            if (_toScene != null)
            {
                return _toScene;
            }
            else
            {
                _toScene = Transform.FindObjectOfType<ToScene>();
                return _toScene;
            }
        }
    }
    public void InitializeCurrentDramaCharacter()
    {
        MainController.mainController.InitializeCurrentDramaCharacter();
    }

    public void ChangeIsSelectedBoard(bool _bool)
    {
        MainController.mainController.IsSelectedBoard = _bool;
    }
    /// <summary>
    /// 按钮事件：开始游戏
    /// </summary>
    public void StartGame()
    {
        MainController.mainController.StartGame();
    }

    public void LoadFile(int _index)
    {
        MainController.mainController.LoadFile(_index);
    }

    public void ReadFile(int _index)
    {
        MainController.mainController.ReadFile(_index);
    }
    /// <summary>
    /// 随便去
    /// </summary>
    /// <param name="_path"></param>
    public void GoTo(string _path)
    {
        MainController.mainController.PlayingVoice(1, Resources.Load<AudioClip>("Voice/Button"));
        SceneManager.LoadScene(_path);

    }

    public void NewFile()
    {
        MainController.mainController.NewFile();
    }

}
