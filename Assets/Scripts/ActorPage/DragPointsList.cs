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
    /// ��������ĸ�����
    /// </summary>
    [SerializeField]
    GameObject parentObj;
    /// <summary>
    /// �����ڵ�
    /// </summary>
    public Sprite sprite;
    public GameObject ActorPoint;
    /// <summary>
    /// �������ڵ��б�
    /// </summary>
    public List<GameObject> actorPoints;
    /// <summary>
    /// ��¼��Ա�ͽ�ɫ��Ϣ
    /// </summary>
    public Dictionary<string, int> actorDic = new Dictionary<string, int>();

    /// <summary>
    /// ��ȡ������С���Ǹ���λ
    /// </summary>
    public GameObject minOne;

    /// <summary>
    /// ��ȡ�����������recttransform���
    /// </summary>
    private MainDrama CurrentDrama;
    /// <summary>
    /// ������ֵ
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
        TotalPerfect.text = "����ֵ�ܺͣ�" + _total;
    }
    /// <summary>
    /// ��ȡ���������һ����λ
    /// </summary>
    /// <param name="_pos">���ֹͣ�϶�ʱ��λ��</param>
    /// <returns>����Ŀ�λ����</returns>
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
