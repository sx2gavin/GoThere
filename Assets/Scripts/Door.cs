using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SwitchTarget
{
    override public void On()
    {
        transform.Rotate(0, 90, 0);
    }
    
    override public void Off()
    {
        transform.Rotate(0, -90, 0);
    }
}
