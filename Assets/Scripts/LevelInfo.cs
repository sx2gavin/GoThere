using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    public string levelSceneName;
    public bool played;
    public int levelIndex;
}
