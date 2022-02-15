using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishArea: MonoBehaviour
{
    public int FedNum;
    Vector2 TargetPos;
    private void Start()
    {
        TargetPos = FindObjectOfType<Leviathan>().gameObject.transform.position;
    }
    private void Update()
    {
        Run();
    }
    void Run()
    {
        if (FedNum >= 3)
        {
            transform.position = Vector2.Lerp(transform.position, TargetPos, Time.deltaTime * 3);
        }
    }
	

	
    
}
