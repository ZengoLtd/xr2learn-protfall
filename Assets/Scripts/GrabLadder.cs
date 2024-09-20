using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLadder : Grabbable
{
    public override void GrabItem(Grabber grabbedBy)
    {
        if (BeingHeld)
        {
            Debug.Log("Grabbed");
            base.GrabItem(grabbedBy);
        }
    }
}
