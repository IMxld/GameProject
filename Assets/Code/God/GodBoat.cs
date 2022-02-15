using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GodBoat : MonoBehaviour
{
    bool IsReach;
    bool isKeyPressed;
    float keyPressedTime;
    bool isPressing;
    bool IsVideoPlaying;
    public GameObject RemindText;
    public RectTransform vector2;
    public GameObject videoPlayer;
    private float f = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Goto(vector2));
        videoPlayer.SetActive(false);

        Chunk.randomNum = new List<int>() { 0, 1, 2, 4, 5, 6, 7 };
        Chunk.randomTemp = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsReach)
        {
            RemindText.SetActive(true);
            PressedSpace();
        }
        if (IsVideoPlaying)
        {
            f += Time.deltaTime;
        }
        if (f > 23)
        {
            SceneManager.LoadScene("FirstScene");
        }
    }
    IEnumerator Goto(RectTransform rectTransform)
    {
        while (Mathf.Abs(gameObject.GetComponent<RectTransform>().position.x - rectTransform.position.x ) > 10 )
        {
            gameObject.GetComponent<RectTransform>().position = Vector2.Lerp(gameObject.GetComponent<RectTransform>().position, rectTransform.position, Time.deltaTime);
            yield return null;
        }
        IsReach = true;
        StopCoroutine("Goto");
    }
    void PressedSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //按键状态为按下
            isKeyPressed = true;
        }

        if (Input.GetKey(KeyCode.Space) && isKeyPressed)
        {
            keyPressedTime += Time.deltaTime;
            if (keyPressedTime > 1 & !isPressing)
            {
                isPressing = true;
                //动画
                videoPlayer.SetActive(true);
                videoPlayer.GetComponent<VideoPlayer>().Play();
                Music.PlayingMusic(0, null);
                //CrossFadeAlpha(1, 0, true);
                //存档
                //切场景
                IsVideoPlaying = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isKeyPressed)
        {
            keyPressedTime = 0;
            isKeyPressed = false;
            isPressing = false;
            
        }
    }
}
