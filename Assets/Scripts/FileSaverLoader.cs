using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileSaverLoader : MonoBehaviour
{
    public LevelSelector[] allLevels;

    private void Start()
    {
    }

    public void Save()
    {
        GameProgress progress = new GameProgress();
        progress.levelsPlayed = new bool[allLevels.Length];
        for(int i = 0; i < allLevels.Length; i++)
        {
            progress.levelsPlayed[i] = allLevels[i].played;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gothere_saved_data.gd");
        bf.Serialize(file, progress);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gothere_saved_data.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gothere_saved_data.gd", FileMode.Open);
            GameProgress progress = (GameProgress)bf.Deserialize(file);
            file.Close();

            LoadProgressToLevels(progress);
        }
    }

    private void LoadProgressToLevels(GameProgress progress)
    {
        for (int i = 0; i < allLevels.Length; i++)
        {
            allLevels[i].played = progress.levelsPlayed[i];
        }
    }
}
