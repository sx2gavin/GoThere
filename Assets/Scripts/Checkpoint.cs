using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerRespawn playerRespawn;

    private void Start()
    {
        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerRespawn.SetNewCheckpoint(this.transform.position);
        }
    }
}
