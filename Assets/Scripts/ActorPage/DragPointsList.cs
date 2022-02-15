using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragPointsList : MonoBehaviour
{
    private static DragPointsList _dragPointsList;
    public static DragPointsList dragPointsList
    {
        get
        {
            if (_dragPointsList != null)
            {
                return _dragPointsList;
            }
            else
            {
                _dragPointsList = Transform.FindObjectOfType<DragPointsList>();
                return _dragPointsList;
            }
        }
    }
    /// <summary>
    /// 挂在物件的父物体
    /// </summary>
    [SerializeField]
    GameObject parentObj;
    /// <summary>
    /// 吸附节点
    /// </summary>
    public Sprite sprite;
    public GameObject ActorPoint;
    /// <summary>
    /// 可吸附节点列表
    /// </summary>
    public List<GameObject> actorPoints;
    /// <summary>
    /// 记录演员和角色信息
    /// </summary>
    public Dictionary<string, int> actorDic = new Dictionary<string, int>();

    /// <summary>
    /// 获取距离最小的那个坑位
    /// </summary>
    public GameObject minOne;

    /// <summary>
    /// 获取所有子物体的recttransform组件
    /// </summary>
    private MainDrama CurrentDrama;
    /// <summary>
    /// 完美总值
    /// </summary>
    public Text TotalPerfect;
    void Start()
    {
        CurrentDrama = MainController.mainController.CurrentDrama.GetComponent<MainDrama>();
        for (int i = 0; i < CurrentDrama.CharactersCount; i++)
        {
            GameObject _item = Instantiate(ActorPoint);
            _item.name += i;
            _item.transform.SetParent(transform);
            _item.GetComponent<Image>().sprite = this.sprite;
            _item.GetComponent<RectTransform>().position = gameObject.transform.GetChild(i).position;
            _item.transform.GetChild(1).GetComponent<Text>().text = CurrentDrama.CharactersNameList[i];
            actorPoints.Add(_item);
            actorDic.Add(_item.name, i);
        }
    }

    private void Update()
    {
        ChangeTotalPerfect();
    }

    private void ChangeTotalPerfect()
    {
        float _total = 0;
        foreach (Character _item in MainController.mainController.CurrentDrama.GetComponent<MainDrama>().CharactersList)
        {
            if (_item != null)
            {
                _total += _item.PerfectValue;
            }
        }
        TotalPerfect.text = "完美值总和：" + _total;
    }
    /// <summary>
    /// 获取距离最近的一个坑位
    /// </summary>
    /// <param name="_pos">鼠标停止拖动时的位置</param>
    /// <returns>最近的坑位坐标</returns>
    public Vector3 GetNearestPoint(Vector3 _pos)
    {
        Vector3 nearestPoint = Vector3.zero;
        float minDistance = float.MaxValue;
        float dis;
        foreach (GameObject point in actorPoints)
        {
            dis = Vector3.Distance(point.transform.position, _pos); 
            if (dis < minDistance)
            {
                minDistance = dis;
                nearestPoint = point.transform.position;
                minOne = point;
            }
        }

        return nearestPoint;
    }
}
