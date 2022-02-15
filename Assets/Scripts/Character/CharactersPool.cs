using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Resource/CharactersPool")]
public class CharactersPool : ScriptableObject
{
    public List<Character> Characters = new List<Character>();
}
