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
    public TextMeshProUGUI currentHitPointText;
    public TextMeshProUGUI maxHitPointText;
    public TextMeshProUGUI instructionText;
    public CameraRig cameraRig;
    public int maxJewelCount = 3;

    private bool hasWon = false;
    private PlayerRespawn playerRespawn;
    private Coroutine instructionCoroutine = null;
    private int jewelCollected = 0;

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

    public void UpdateCurrentHitPoint(int hitPoints)
    {
        if (hitpointsDisplay != null)
        {
            hitpointsDisplay.UpdateHitpoints(hitPoints);
        }
    }

    public void UpdateMaxHitPoint(int maxHitPoints)
    {
        if (maxHitPointText != null)
        {
            maxHitPointText.text = maxHitPoints.ToString();
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

    public void CollectJewel()
    {
        jewelCollected++;
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
