using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealer : MonoBehaviour
{
    public int damage = 1;
    public GameObject bloodVFX;

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

                var contactPoint = (myCenter + otherCenter) / 2;
                var blood = Instantiate(bloodVFX, otherCenter, Quaternion.identity);
                Destroy(blood, 10.0f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        collider = GetComponent<Collider>();
        Gizmos.DrawSphere(collider.bounds.center, 0.1f);
    }
}
