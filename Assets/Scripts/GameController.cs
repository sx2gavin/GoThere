using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject deathTextOverlay;
    public GameObject winTextOverlay;
    public TextMeshProUGUI currentHitPointText;
    public TextMeshProUGUI maxHitPointText;
    public CameraRig cameraRig;
    
    private bool hasWon = false;

    private void Start()
    {
        if (deathTextOverlay)
        {
            deathTextOverlay.SetActive(false);
        }
    }

    private void Update()
    {
        if (hasWon)
        {
            if (Input.GetAxis("Submit") > 0.0f)
            {
                // TODO: load next level.
            }
            else if (Input.GetAxis("Cancel") > 0.0f)
            {
                SceneLoader.Instance.LoadLevelSelectionScene();
            }
        }
    }

    public void Win()
    {
        if (winTextOverlay)
        {
            winTextOverlay.SetActive(true);
            hasWon = true;
        }
    }

    public void UpdateCurrentHitPoint(int hitPoints)
    {
        if (currentHitPointText != null)
        {
            currentHitPointText.text = hitPoints.ToString();
        }
    }

    public void UpdateMaxHitPoint(int maxHitPoints)
    {
        if (maxHitPointText != null)
        {
            maxHitPointText.text = maxHitPoints.ToString();
        }
    }

    public void PlayerIsDead()
    {
        if (deathTextOverlay)
        {
            deathTextOverlay.SetActive(true);
        }

        cameraRig?.Detach();

        StartCoroutine(ResetSceneAfter3Seconds());
    }

    private IEnumerator ResetSceneAfter3Seconds()
    {
        yield return new WaitForSeconds(2.0f);
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
