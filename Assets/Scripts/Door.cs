using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SwitchTarget
{
    override public void On()
    {
        base.On();
        transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
    
    override public void Off()
    {
        base.Off();
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
