using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepSamples;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomFootStepSample()
    {
        int sampleIndex = Random.Range(0, footStepSamples.Length - 1);

        audioSource.PlayOneShot(footStepSamples[sampleIndex]);
    }
}
