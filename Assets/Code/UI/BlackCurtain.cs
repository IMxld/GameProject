using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BlackCurtain : MonoBehaviour
{
    public GameObject Player;
    public GameObject Book;
    public GameObject CurtainTop;
    public GameObject CuttainDown;
    public GameObject Video;
    public GameObject GameoverVideo;
    public static BlackCurtain _blackCurtain;
    public bool IsClosing;
    public bool IsGameOver;
    public int Speed;
    private float y;
    private int i = 1;
    private float f;

    public static BlackCurtain instance
    {
        get
        {
            if (_blackCurtain != null)
            {
                return _blackCurtain;
            }
            else
            {
                _blackCurtain = Transform.FindObjectOfType<BlackCurtain>();
                return _blackCurtain;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LateUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Close();
        GameOver();
    }

    void Close()
    {
        if (IsClosing)
        {
            CurtainTop.transform.localPosition = Vector2.Lerp(CurtainTop.transform.localPosition, new Vector2(0, -440), Time.deltaTime * Speed);
            CuttainDown.transform.localPosition = Vector2.Lerp(CuttainDown.transform.localPosition, new Vector2(0, 440), Time.deltaTime * Speed);
            Video.GetComponent<RawImage>().CrossFadeAlpha(1, 0f, true);
            Book.SetActive(false);
            Player.SetActive(false);
            f += Time.deltaTime;
            if (f > 8)
            {
                IsClosing = false;
                Book.SetActive(true);
                Player.SetActive(true);
                Player.GetComponent<PlayerController>().canMove = true;
                IsClosing = false;
                f = 0;
            }

        }
        else
        {
            CurtainTop.transform.localPosition = Vector2.Lerp(CurtainTop.transform.localPosition, new Vector2(0, -645), Time.deltaTime * Speed);
            CuttainDown.transform.localPosition = Vector2.Lerp(CuttainDown.transform.localPosition, new Vector2(0, 645), Time.deltaTime * Speed);
            Video.GetComponent<RawImage>().CrossFadeAlpha(0 ,0f, true);
        }

    }

    void GameOver()
    {
        if (IsGameOver)
        {
            GameoverVideo.GetComponent<RawImage>().CrossFadeAlpha(1, 1f, true);
            Book.SetActive(false);
            Player.SetActive(false);
            y += Time.deltaTime;
            if (y > 7)
            {
                SceneManager.LoadScene("FirstScene");
            }
        }
        else
        {
            GameoverVideo.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);

        }
    }

    
    
}
