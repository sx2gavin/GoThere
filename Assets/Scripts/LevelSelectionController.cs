using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour
{
    public LevelSelector[] allLevels;

    public void SaveAllLevelInfoToFile()
    {
        GameProgress progress = new GameProgress();
        progress.levelsPlayed = new bool[allLevels.Length];
        for (int i = 0; i < allLevels.Length; i++)
        {
            progress.levelsPlayed[i] = allLevels[i].played;
        }

        FileSaverLoader.Save("gothere_saved_data.gd", progress);
    }

    private void Start()
    {
        LoadGameDataToScene();
    }

    private void LoadGameDataToScene()
    {
        if (GameData.Instance.gameProgress != null)
        {
            for (int i = 0; i < allLevels.Length; i++)
            {
                allLevels[i].SetPlayedFlag(GameData.Instance.gameProgress.levelsPlayed[i]);
            }
        }
    }
}
