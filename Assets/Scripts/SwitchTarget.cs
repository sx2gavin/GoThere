using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchTarget : MonoBehaviour
{
    private bool isOn = false;
    public bool IsOn()
    {
        return isOn;
    }

    public virtual void On()
    {
        isOn = true;
    }

    public virtual void Off()
    {
        isOn = false;
    }

}
