using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �л�ȫ���ʹ���ģʽ
/// </summary>
public class WindowsFullscreen : MonoBehaviour
{
    //�л�
    private bool switchover;

    private void Start()
    {
        switchover = true;
        Screen.SetResolution(1920, 1080, switchover);
    }

    void Update()
    {
        //  ��Control�л�ȫ�����ߴ���ģʽ      
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            switchover = !switchover;
            if (switchover)
            {
                Screen.SetResolution(1920, 1080, switchover);
            }
            else
            {
                Screen.SetResolution(800, 600, switchover);
            }
            Screen.fullScreen = switchover;
        }

    }
}