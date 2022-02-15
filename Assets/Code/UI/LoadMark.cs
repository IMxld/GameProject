using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMark : MonoBehaviour
{
    float MoveX = 28.1f;
    float MoveY = 14.3f;
    public Vector3 OriginalPos;
    public Vector3 offset;
    public List<GameObject> Points;//left,right,up,down,
    // Start is called before the first frame update

    //移动
    private void Start()
    {

    }

    public void LoadLines()
    {
        transform.GetComponent<RectTransform>().localPosition = OriginalPos + offset;
        for (int i = 0; i < Save.walked.Count - 1; i++)
        {
            if (Save.walked[i + 1].x > Save.walked[i].x)
            {
                if (Save.walked[i + 1].y > Save.walked[i].y)
                {
                    Move(2);
                }
                else
                {
                    Move(1);
                }
            }
            else
            {
                if (Save.walked[i + 1].y > Save.walked[i].y)
                {
                    Move(0);
                }
                else
                {
                    Move(3);
                }
            }
        }
    }
    public void DropLines()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Road"))
        {
            Destroy(item,1);
        }
        transform.GetComponent<RectTransform>().localPosition = OriginalPos + offset;
    }
    public void Move(int _direction)
    {
        Vector3 TargetPos = transform.GetComponent<RectTransform>().localPosition;
        switch (_direction)
        {
            case 0:
                TargetPos += new Vector3(-MoveX, MoveY);
                InstantiatePoint(_direction, transform.GetComponent<RectTransform>().localPosition - offset);
                InstantiatePoint(Opposite(_direction), TargetPos - offset);
                break;
            case 1:
                TargetPos += new Vector3(MoveX, -MoveY);
                InstantiatePoint(_direction, transform.GetComponent<RectTransform>().localPosition - offset);
                InstantiatePoint(Opposite(_direction), TargetPos - offset);
                break;
            case 2:
                TargetPos += new Vector3(MoveX, MoveY);
                InstantiatePoint(_direction, transform.GetComponent<RectTransform>().localPosition - offset);
                InstantiatePoint(Opposite(_direction), TargetPos - offset);
                break;
            default:
                TargetPos += new Vector3(-MoveX, -MoveY);
                InstantiatePoint(_direction, transform.GetComponent<RectTransform>().localPosition - offset);
                InstantiatePoint(Opposite(_direction), TargetPos - offset);
                break;
        }
        transform.GetComponent<RectTransform>().localPosition = TargetPos;
    }
    //在原地留下一个红点
    public void InstantiatePoint(int _direction, Vector2 _Pos)
    {
        GameObject gameObject = Instantiate(Points[_direction]);
        gameObject.transform.SetParent(transform.parent);
        gameObject.transform.GetComponent<RectTransform>().localPosition = _Pos;



    }
    int Opposite(int _index)
    {
        if (_index == 0)
        {
            return 1;
        }
        else if (_index == 1)
        {
            return 0;
        }
        else if (_index == 2)
        {
            return 3;
        }
        else
        {
            return 2;
        }
    }
}
