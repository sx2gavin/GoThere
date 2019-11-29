using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileSaverLoader
{
    public static void Save(string filename, GameProgress gameProgress)
    {
    //    GameProgress progress = new GameProgress();
    //    progress.levelsPlayed = new bool[allLevels.Length];
    //    for(int i = 0; i < allLevels.Length; i++)
    //    {
    //        progress.levelsPlayed[i] = allLevels[i].played;
    //    }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
        bf.Serialize(file, gameProgress);
        file.Close();
    }

    public static GameProgress Load(string filename)
    {
        string fullPath = Application.persistentDataPath + "/" + filename;

        if (File.Exists(fullPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPath, FileMode.Open);
            GameProgress progress = (GameProgress)bf.Deserialize(file);
            file.Close();
            return progress;
        }
        else
        {
            return null;
        }
    }
}
