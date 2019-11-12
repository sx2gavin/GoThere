using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Renderer))]
public class Jewel : MonoBehaviour
{
    private GameController gameController;
    private AudioSource audioSource;
    private Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameController.CollectJewel();
            audioSource.Play();
            meshRenderer.enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
