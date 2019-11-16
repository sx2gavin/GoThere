using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepSamples;
    public GameObject footDust;
    public GameObject rightFoot;
    public GameObject leftFoot;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StepLeft()
    {
        PlayRandomFootStepSample();
        InstantiateFootDust(leftFoot.transform.position);
    }

    public void StepRight()
    {
        PlayRandomFootStepSample();
        InstantiateFootDust(rightFoot.transform.position);
    }

    private void PlayRandomFootStepSample()
    {
        int sampleIndex = Random.Range(0, footStepSamples.Length - 1);

        audioSource.PlayOneShot(footStepSamples[sampleIndex]);
    }

    private void InstantiateFootDust(Vector3 footLocation)
    {
        var footDustInstance = Instantiate(footDust, footLocation, Quaternion.identity);
        Destroy(footDustInstance, 1f);
    }
}
