using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    
    public static AudioSource BGM;
    public static AudioSource Effect;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponents<AudioSource>()[0];
        Effect = GetComponents<AudioSource>()[1];
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Load());
        //PlayingMusic(0, Resources.Load<AudioClip>("Audio/开始音乐1"));
    }

    private void Update()
    {
        BGM.volume = Save.files.options.allMusic * Save.files.options.music;
        Effect.volume = Save.files.options.allMusic * Save.files.options.voice;
    }

    /// <summary>
    /// 0为BGM,1为效果
    /// </summary>
    /// <param name="_num"></param>
    /// <param name="audioClip"></param>
    public static void PlayingMusic(int _num, AudioClip audioClip)
    {
        if (_num == 0)
        {
            BGM.clip = audioClip;
            
            BGM.Play();
        }
        else
        {
            Effect.clip = audioClip;
            
            Effect.Play();
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FirstScene");
    }
}
