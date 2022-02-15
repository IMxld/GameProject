using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource/CharactersLibrary")]
public class CharactersLibrary : ScriptableObject
{
    public List<ActorFile> files = new List<ActorFile>();
}
[System.Serializable]
public class ActorFile
{
    public int FileNume;
    public List<Character> characters = new List<Character>();
}
