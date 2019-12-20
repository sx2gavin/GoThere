using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject deathTextOverlay;
    public Hitpoints hitpointsDisplay;
    public TextMeshProUGUI instructionText;
    public CameraRig cameraRig;

    private bool hasWon = false;
    private PlayerRespawn playerRespawn;
    private Coroutine instructionCoroutine = null;

    private void Start()
    {
        if (deathTextOverlay)
        {
            deathTextOverlay.SetActive(false);
        }

        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    public void Cleanup()
    {
        Destroy(playerRespawn.gameObject);
    }

    public void UpdateCurrentHitpoint(int hitPoints)
    {
        if (hitpointsDisplay != null)
        {
            hitpointsDisplay.UpdateHitpoints(hitPoints);
        }
    }

    public void UpdateInstructionText(string instruction)
    {
        if (instructionCoroutine != null)
        {
            StopCoroutine(instructionCoroutine);
        }
        instructionText.text = instruction;
        instructionText.gameObject.SetActive(true);
        instructionCoroutine = StartCoroutine(DelayHideInstructionText());
    }

    private IEnumerator DelayHideInstructionText()
    {
        yield return new WaitForSeconds(5.0f);
        instructionText.gameObject.SetActive(false);
        instructionText.text = "";
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
