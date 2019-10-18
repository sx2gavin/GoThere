using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string levelSelectionSceneName = "Level Selection";
    public string mainMenuSceneName = "Start Menu";

    private static SceneLoader instance;
    public static SceneLoader Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadLevelSelectionScene()
    {
        LoadScene(levelSelectionSceneName);
    }

    public void LoadMainMenuScene()
    {
        LoadScene(mainMenuSceneName);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
