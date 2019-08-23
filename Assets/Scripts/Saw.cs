﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public GameObject sawModel;
    public Transform startPosition;
    public Transform endPosition;
    public float rotationSpeed = 10f;
    public float movingSpeed = 10f;
    public float pauseDuration = 1f;

    private Transform currentDestination;
    private Transform nextDestination;
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = endPosition;
        nextDestination = startPosition;
        StartCoroutine(MoveBackAndForth());
    }

    private IEnumerator MoveBackAndForth()
    {
        while (true)
        {
            if (sawModel.transform.position != currentDestination.position)
            {
                var direction = currentDestination.position - sawModel.transform.position;
                var moveDistance = direction.normalized * movingSpeed * Time.deltaTime;
                sawModel.transform.position += moveDistance;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                var temp = nextDestination;
                nextDestination = currentDestination;
                currentDestination = temp;
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        sawModel.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
