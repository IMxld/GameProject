using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    //按键是否被按下
    private bool isKeyPressed = false;
    //按下那一刻的时间
    private float keyPressedTime;
    //按键抬起的那一瞬间
    private float keyUpTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //按键按下时记录按下时间
            keyPressedTime = Time.time;
            //按键状态为按下
            isKeyPressed = true;
        }

        if (Input.GetKey(KeyCode.Space) && isKeyPressed)
        {
            //记录按键抬起时间
            keyUpTime = Time.time;
            if (keyUpTime - keyPressedTime > 1.0f)
            {
                //如果按键时间超过1s，则按下状态变为false
                isKeyPressed = false;

                //长按要执行的事件写在这里

            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isKeyPressed)
        {
            //记录按键抬起时间
            keyUpTime = Time.time;
            if (keyUpTime - keyPressedTime <= 1.0f)
            {
                //如果按键时间不超过1s，则按下状态变为false
                isKeyPressed = false;

                //短按要执行的事件写在这里

            }
        }
    }
}
