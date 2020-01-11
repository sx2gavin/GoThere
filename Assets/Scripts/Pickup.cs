using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{
    public Collider collider;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    public void PickedUp()
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }

    public void Dropped()
    {
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

}
