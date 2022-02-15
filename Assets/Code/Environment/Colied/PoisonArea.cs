using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public static PoisonArea _poison;
    public static PoisonArea _Poison
    {
        get
        {
            if (_poison)
            {
                return _poison;
            }
            else
            {
                _poison = Transform.FindObjectOfType<PoisonArea>();
                return _poison;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
