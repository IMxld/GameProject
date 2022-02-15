using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActorSelectBoard : MonoBehaviour
{
    /// <summary>
    /// ��Աѡ�����ʵ��
    /// </summary>
    private static ActorSelectBoard _actorSelectBoard;
    public static ActorSelectBoard actorSelectBoard
    {
        get
        {
            if (_actorSelectBoard == null)
            {
                _actorSelectBoard = Transform.FindObjectOfType<ActorSelectBoard>();
                return _actorSelectBoard;
            }
            else
            {
                return _actorSelectBoard;
            }
        }
    }
    public Image DramaBackground;//�籾����
    public Text DramaName;//�籾��
    public Text DramaIntroduction;//�籾���
    public GameObject RemainText;//�����ı�
    /// <summary>
    /// ��ǰ�籾
    /// </summary>
    private MainDrama CurrentDrama;
    void Start()
    {
        LoadInfomation();
    }
    public void OnMouseDown()
    {
        SceneManager.LoadScene("SelectedPage");
    }
    public void LoadInfomation()
    {
        CurrentDrama = GameObject.Find("MainController").GetComponent<MainController>().CurrentDrama.GetComponent<MainDrama>();
        if (CurrentDrama != null)
        {
            DramaBackground.sprite = CurrentDrama.ImageBackground;
            DramaName.text = CurrentDrama.Name;
            DramaIntroduction.text = CurrentDrama.Plot;
        }
    }

    public void LoadDrama()
    {
        bool _canLoad = true;
        foreach (Character _gameObject in MainController.mainController.CurrentDrama.GetComponent<MainDrama>().CharactersList)//�ж��ܷ��ݳ�
        {
            if (_gameObject.PerfectValue == 0)
            {
                _canLoad = false;
            }
        }

        if (_canLoad)
        {
            MainController.mainController.LoadDrama();
            ToScene.toScene.GoTo("SelectedPage");
        }
        else
        {
            StartCoroutine(Remain());
        }

    }

    IEnumerator Remain()
    {
        RemainText.SetActive(true);
        yield return new WaitForSeconds(.5f);
        RemainText.SetActive(false);
    }
}
