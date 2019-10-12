using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float buttonPressDistance = 2f;
    public SwitchTarget target;

    private void OnTriggerEnter(Collider other)
    {
        target?.On();
        transform.Translate(Vector3.down * buttonPressDistance);
    }

    private void OnTriggerExit(Collider other)
    {
        target?.Off();
        transform.Translate(Vector3.up * buttonPressDistance);
    }
}
