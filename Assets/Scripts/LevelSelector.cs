using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelInfoCanvas;
    public TextMeshProUGUI levelText;
    public GameObject playedText;
    public string levelSceneName;
    public bool played;


    private bool playerIsNear = false;

    private void Start()
    {
        if (levelInfoCanvas != null)
        {
            levelInfoCanvas.SetActive(true);
        }

        if (levelText != null)
        {
            levelText.SetText(levelSceneName);
            playedText.SetActive(played);
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

    /*
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
    */
}
