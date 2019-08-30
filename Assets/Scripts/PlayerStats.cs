using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Tooltip("The amount of time the player is invincible after damage.")]
    public float invincibleDuration = 1f;
    public int currentLife = 3;
    public int maxLife = 3;
    public Material meshMaterial;

    private bool isInvincible;
    private GameController gameController;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();    
    }

    public bool TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentLife -= damage;
            gameController.UpdateCurrentHitPoint(currentLife);
            StartCoroutine(Invincible());
            if (currentLife == 0)
            {
                gameController.PlayerIsDead();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    // Let the player be invincible for a short time.
    private IEnumerator Invincible()
    {
        meshMaterial.color = Color.red;
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        meshMaterial.color = Color.white;
        isInvincible = false;
    }
}
