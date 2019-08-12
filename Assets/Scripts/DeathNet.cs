using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathNet : MonoBehaviour
{
    GameController _gameController;

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _gameController?.PlayerIsDead();
        }
    }
}
