using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coiled : MonoBehaviour
{
    public static Coiled _sevenDragon;
    public static Coiled instance
    {
        get
        {
            if (_sevenDragon)
            {
                return _sevenDragon;
            }
            else
            {
                _sevenDragon = Transform.FindObjectOfType<Coiled>();
                return _sevenDragon;
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
