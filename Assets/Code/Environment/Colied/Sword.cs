using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static Sword _swords;
    public static Sword instance
    {
        get
        {
            if (_swords)
            {
                return _swords;
            }
            else
            {
                _swords = Transform.FindObjectOfType<Sword>();
                return _swords;
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
