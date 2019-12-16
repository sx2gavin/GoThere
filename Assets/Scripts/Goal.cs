using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    WinMenu winMenu;

    private void Start()
    {
        winMenu = FindObjectOfType<WinMenu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            winMenu?.Win();
        }
    }
}
