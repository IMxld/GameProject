using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoChange : MonoBehaviour
{
    [SerializeField]
    public Sprite img1, img2;

    private int count = 0;

    public void OnMouseDown()
    {
        if (count % 2 == 0)
        {
            this.GetComponent<Image>().sprite = img2;
        }
        else
        {
            this.GetComponent<Image>().sprite = img1;
        }
        count++;
    }
}
