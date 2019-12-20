using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerStats>();
        if (player)
        {
            player.GainLife(1);
            Destroy(gameObject);
        }
    }
}
