using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Resource/EntryPool")]
public class EntryPool:ScriptableObject
{
    public List<GameObject> entries = new List<GameObject>();

}

