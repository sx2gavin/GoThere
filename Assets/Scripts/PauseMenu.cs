using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject firstMenuItem;

    private void Start()
    {
        pauseMenu?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (pauseMenu == null)
            {
                Debug.LogError("You forgot to add pause menu.");
            }
            bool currentActiveState = pauseMenu.activeSelf;
            pauseMenu?.SetActive(!currentActiveState);
            
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0.0f;
                EventSystem.current.SetSelectedGameObject(firstMenuItem);
            }
            else
            {
                Time.timeScale = 1.0f;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
