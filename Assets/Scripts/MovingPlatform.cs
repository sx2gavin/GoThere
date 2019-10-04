using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public Transform startPosition;
    public Transform endPosition;
    public float movingSpeed = 10f;
    public float pauseDuration = 1f;

    private Transform currentDestination;
    private Transform currentDeparture;
    private float distance;
    private bool isPausing = false;
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = endPosition;
        currentDeparture = startPosition;
        distance = Vector3.Distance(startPosition.position, endPosition.position);
    }

    private IEnumerator MoveBackAndForth()
    {
        while (true)
        {
            var currentDistance = Vector3.Distance(platform.transform.position, currentDeparture.position);
            if (currentDistance < distance)
            {
                var direction = currentDestination.position - platform.transform.position;
                var moveDistance = direction.normalized * movingSpeed * Time.deltaTime;
                platform.transform.position += moveDistance;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                var temp = currentDeparture;
                currentDeparture = currentDestination;
                currentDestination = temp;
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPausing)
        {
            return;
        }

        var currentDistance = Vector3.Distance(platform.transform.position, currentDeparture.position);
        if (currentDistance < distance)
        {
            var direction = currentDestination.position - platform.transform.position;
            var moveDistance = direction.normalized * movingSpeed * Time.deltaTime;
            platform.transform.position += moveDistance;
        }
        else
        {
            var temp = currentDeparture;
            currentDeparture = currentDestination;
            currentDestination = temp;
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        isPausing = true;
        yield return new WaitForSeconds(pauseDuration);
        isPausing = false;
    }
}
