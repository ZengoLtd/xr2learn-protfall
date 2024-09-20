using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticktoPosition : MonoBehaviour
{
    public Transform stickTo;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = stickTo.position;
    }
}
