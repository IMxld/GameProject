using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    //�����Ƿ񱻰���
    private bool isKeyPressed = false;
    //������һ�̵�ʱ��
    private float keyPressedTime;
    //����̧�����һ˲��
    private float keyUpTime;

    private void Start()
    {
        Music.PlayingMusic(0, Resources.Load<AudioClip>("Audio/��ʼ����1"));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //��������ʱ��¼����ʱ��
            keyPressedTime = Time.time;
            //����״̬Ϊ����
            isKeyPressed = true;
        }

        if (Input.GetKey(KeyCode.Space) && isKeyPressed)
        {
            //��¼����̧��ʱ��
            keyUpTime = Time.time;
            if (keyUpTime - keyPressedTime > 1.0f)
            {
                //�������ʱ�䳬��1s������״̬��Ϊfalse
                isKeyPressed = false;
                SceneManager.LoadScene("SampleScene");

            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isKeyPressed)
        {
            //��¼����̧��ʱ��
            keyUpTime = Time.time;
            if (keyUpTime - keyPressedTime <= 1.0f)
            {
                //�������ʱ�䲻����1s������״̬��Ϊfalse
                isKeyPressed = false;

                //�̰�Ҫִ�е��¼�д������

            }
        }
    }
}
