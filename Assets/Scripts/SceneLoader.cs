using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

    public string levelSelectionSceneName = "Level Selection";
    public void LoadLevelSelectionScene()
    {
        SceneManager.LoadScene(levelSelectionSceneName, LoadSceneMode.Single);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
