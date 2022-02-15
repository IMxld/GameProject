using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    private static Curtain _curtain;
    public static Curtain curtain
    {
        get
        {
            if (_curtain != null) 
            {
                return _curtain;
            }
            else
            {
                _curtain = Transform.FindObjectOfType<Curtain>();
                return _curtain;
            }
        }
    }
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void CurtainOpen()
    {
        animator.SetTrigger("Open");
    }
    public void CurtainClose()
    {
        animator.SetTrigger("Close");
    }
}
