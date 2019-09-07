using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelInfoCanvas;
    public string levelSceneName;

    private bool playerIsNear = false;

    private void Start()
    {
        if (levelInfoCanvas != null)
        {
            levelInfoCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerIsNear)
        {
            if (Input.GetAxis("Submit") > 0)
            {
                SceneLoader.Instance?.LoadScene(levelSceneName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelInfoCanvas?.SetActive(true);
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            levelInfoCanvas?.SetActive(false);
            playerIsNear = false;
        }
    }
}
