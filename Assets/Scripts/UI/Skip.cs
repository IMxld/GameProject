using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    /// <summary>
    /// ���㵯��
    /// </summary>
    [SerializeField]
    Canvas settlementCanvas;

    /// <summary>
    /// ��������
    /// </summary>
    [SerializeField]
    Canvas parentCanvas;

    /// <summary>
    /// �������Ƶ���������ȥ
    /// </summary>
    private void Awake()
    {
        //ԭ���ķ���
        //settlementCanvas.transform.localPosition = new Vector3(0, parentCanvas.GetComponent<RectTransform>().rect.height, 0);
    }

    //�籾�������ʱǿ�ƽ���������
    private void Update()
    {
        if(Playing.isend)
        {
            OnMouseDown();
            Playing.isend = !Playing.isend;
        }
    }

    /// <summary>
    /// �������ƻ�������skip����Ϊ������
    /// </summary>
    public void OnMouseDown()
    {
        //ԭ���ķ���
        //InvokeRepeating("Show", 3 * Time.deltaTime, Time.deltaTime);
        //this.transform.localScale -= new Vector3(0.1f, 0.1f);
        //this.gameObject.SetActive(false);
        settlementCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    ///// <summary>
    ///// �������ַ���
    ///// </summary>
    //private void Show()
    //{
    //    settlementCanvas.transform.localPosition = Vector3.Lerp(settlementCanvas.transform.localPosition, Vector3.zero, 3 * Time.deltaTime);
    //}
}
