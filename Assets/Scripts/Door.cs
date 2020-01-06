using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SwitchTarget
{
    public GameObject doorModel;

    override public void On()
    {
        base.On();
        if (doorModel)
        {
            doorModel.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }
    
    override public void Off()
    {
        base.Off();
        if (doorModel)
        {
            doorModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
