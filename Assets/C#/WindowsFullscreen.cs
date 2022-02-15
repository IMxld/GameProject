using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 切换全屏和窗口模式
/// </summary>
public class WindowsFullscreen : MonoBehaviour
{
    //切换
    private bool switchover;

    private void Start()
    {
        switchover = true;
        Screen.SetResolution(1920, 1080, switchover);
    }

    void Update()
    {
        //  按Control切换全屏或者窗口模式      
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