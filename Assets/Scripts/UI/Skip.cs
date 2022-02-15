using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    /// <summary>
    /// 结算弹窗
    /// </summary>
    [SerializeField]
    Canvas settlementCanvas;

    /// <summary>
    /// 整个画面
    /// </summary>
    [SerializeField]
    Canvas parentCanvas;

    /// <summary>
    /// 将弹窗移到画面外面去
    /// </summary>
    private void Awake()
    {
        //原来的方案
        //settlementCanvas.transform.localPosition = new Vector3(0, parentCanvas.GetComponent<RectTransform>().rect.height, 0);
    }

    //剧本播放完毕时强制进入结算界面
    private void Update()
    {
        if(Playing.isend)
        {
            OnMouseDown();
            Playing.isend = !Playing.isend;
        }
    }

    /// <summary>
    /// 将弹窗移回来并将skip键设为不可用
    /// </summary>
    public void OnMouseDown()
    {
        //原来的方案
        //InvokeRepeating("Show", 3 * Time.deltaTime, Time.deltaTime);
        //this.transform.localScale -= new Vector3(0.1f, 0.1f);
        //this.gameObject.SetActive(false);
        settlementCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    ///// <summary>
    ///// 弹窗出现方法
    ///// </summary>
    //private void Show()
    //{
    //    settlementCanvas.transform.localPosition = Vector3.Lerp(settlementCanvas.transform.localPosition, Vector3.zero, 3 * Time.deltaTime);
    //}
}
