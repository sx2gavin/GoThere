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
            bool tookDamage = playerStats.TakeDamage(damage);

            if (tookDamage)
            {
                var player = other.GetComponent<PlayerMovement>();
                var knockBackDirection = (other.transform.position - transform.position).normalized;
                player.KnockBack(knockBackDirection);
            }
        }
    }
}
