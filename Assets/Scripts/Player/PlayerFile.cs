using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Resource/PlayerFile")]
[System.Serializable]
public class PlayerFile : ScriptableObject
{
    public List<PlayerController> playerControllers = new List<PlayerController>();
}

