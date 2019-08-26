using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
        }
    }
}
