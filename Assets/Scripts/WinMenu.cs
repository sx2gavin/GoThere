using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinMenu : MonoBehaviour
{
    public GameObject winTextOverlay;
    public GameObject nextLevelButton;

    public void Win()
    {
        if (winTextOverlay)
        {
            winTextOverlay.SetActive(true);
            EventSystem.current.SetSelectedGameObject(nextLevelButton);
        }
    }
}
