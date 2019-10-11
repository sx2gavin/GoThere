using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealer : MonoBehaviour
{
    public int damage = 1;

    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerStats = other.GetComponent<PlayerStats>();
            bool tookDamage = playerStats.TakeDamage(damage);

            if (tookDamage)
            {
                var player = other.GetComponent<PlayerMovement>();
                var otherCenter = other.bounds.center;
                var myCenter = collider.bounds.center;
                var direction = otherCenter - myCenter;
                direction.Normalize();
                direction += Vector3.up;
                direction.Normalize();
                player.KnockBack(direction);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        collider = GetComponent<Collider>();
        Gizmos.DrawSphere(collider.bounds.center, 0.1f);
    }
}
