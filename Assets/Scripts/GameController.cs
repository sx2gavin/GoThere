using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject _deathTextOverlay;

    [SerializeField]
    GameObject _winTextOverlay;

    [SerializeField]
    CameraRig _cameraRig;

    private void Start()
    {
        if (_deathTextOverlay)
        {
            _deathTextOverlay.SetActive(false);
        }
    }

    public void Win()
    {
        if (_winTextOverlay)
        {
            _winTextOverlay.SetActive(true);
        }
    }

    public void PlayerIsDead()
    {
        if (_deathTextOverlay)
        {
            _deathTextOverlay.SetActive(true);
        }

        _cameraRig?.Detach();

        StartCoroutine(ResetSceneAfter3Seconds());
    }

    private IEnumerator ResetSceneAfter3Seconds()
    {
        yield return new WaitForSeconds(2.0f);
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
