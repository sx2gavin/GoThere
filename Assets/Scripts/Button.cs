using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float buttonPressDistance = 0.4f;
    public GameObject knob;
    public SwitchTarget target;

    private void OnTriggerStay(Collider other)
    {
        if (target && target.IsOn() == false)
        {
            target.On();
            knob?.transform.Translate(Vector3.down * buttonPressDistance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target && target.IsOn() == true)
        {
            target.Off();
            knob?.transform.Translate(Vector3.up * buttonPressDistance);
        }
    }
}
