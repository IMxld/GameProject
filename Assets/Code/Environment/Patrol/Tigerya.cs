using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tigerya : MonoBehaviour
{
    public static Tigerya _tigerya;
    public static Tigerya Instance
    {
        get
        {
            if (_tigerya)
            {
                return _tigerya;
            }
            else
            {
                _tigerya = Transform.FindObjectOfType<Tigerya>();
                return _tigerya;
            }
        }
    }
    public Vector2 Center;
    public List<Vector2> PatrolPos;
    public int PatrolNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        StartCoroutine(Patroling(PatrolPos[PatrolNum]));
        PatrolNum++;
        if (PatrolNum >= PatrolPos.Count)
        {
            PatrolNum = 0;
        }
    }

    
    IEnumerator Patroling(Vector2 _target)
    {
        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, _target, Time.deltaTime);
            yield return null;
        }
    }
}
