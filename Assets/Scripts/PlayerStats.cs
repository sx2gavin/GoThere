using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentLife = 3;
    public int maxLife = 3;

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
    }
}
