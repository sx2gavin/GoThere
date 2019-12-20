using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Renderer))]
public class Jewel : MonoBehaviour
{
    private JewelCount jewelCount;
    private AudioSource audioSource;
    private Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        jewelCount = FindObjectOfType<JewelCount>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jewelCount?.CollectJewel();
            audioSource.Play();
            meshRenderer.enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
