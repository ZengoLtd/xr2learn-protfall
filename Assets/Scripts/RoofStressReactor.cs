using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofStressReactor : StressReactor
{
    bool onroof = false;
    bool lastState = false;
    public void OnRoof(){
        onroof = true;
        Toggle(lastState);
    }

    public void OffRoof(){
        onroof = false;
        Toggle(lastState);
    }

    protected override void Toggle(bool state){
        lastState = state;
        bool floor = false;
        if(state && onroof){
             floor = true;
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(floor);
        }
    }

}
