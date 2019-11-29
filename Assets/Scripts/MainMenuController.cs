using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject continueMenuOption;
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/gothere_saved_data.gd"))
        {
            GameProgress progress = FileSaverLoader.Load("gothere_saved_data.gd");

            GameData.Instance.gameProgress = progress;
            if (continueMenuOption)
            {
                continueMenuOption.SetActive(true);
            }
        }
        else
        {
            if (continueMenuOption)
            {
                continueMenuOption.SetActive(false);
            }
        }
    }

    public void LoadLevelSelectionScene(bool isContinue)
    {
        if (!isContinue)
        {
            GameData.Instance.gameProgress = null;
        }
        SceneLoader.Instance.LoadLevelSelectionScene();
    }
}
