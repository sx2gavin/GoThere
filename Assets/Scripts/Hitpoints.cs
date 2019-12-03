using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitpoints : MonoBehaviour
{
    public Image[] hearts;

    private int fullHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        fullHealth = hearts.Length;
        currentHealth = hearts.Length;
    }

    public void LoseHealth(int health)
    {
        currentHealth -= health;
        int i = 0;
        for (i = 0; i < currentHealth; i++)
        {
            hearts[i].color = new Color32(255, 120, 120, 255);
        }

        for (; i < fullHealth; i++)
        {
            hearts[i].color = new Color32(70, 70, 70, 255);
        }
    }

    public void GainHealth(int health)
    {
        currentHealth += health;
        int i = 0;
        for (i = 0; i < currentHealth; i++)
        {
            hearts[i].color = new Color32(255, 120, 120, 255);
        }

        for (; i < fullHealth; i++)
        {
            hearts[i].color = new Color32(70, 70, 70, 255);
        }
    }

    public void UpdateHitpoints(int hitpoints)
    {
        currentHealth = hitpoints;
        int i = 0;
        for (i = 0; i < currentHealth; i++)
        {
            hearts[i].color = new Color32(255, 120, 120, 255);
        }

        for (; i < fullHealth; i++)
        {
            hearts[i].color = new Color32(70, 70, 70, 255);
        }
    }
}
