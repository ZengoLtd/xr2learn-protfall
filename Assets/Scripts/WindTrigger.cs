using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindTrigger : MonoBehaviour
{
    public UnityEvent OnWindStart;
    public UnityEvent OnWindStop;
    public UnityEvent OnWindStay;

    private void OnTriggerEnter(Collider other)
    {
        OnWindStart.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnWindStop.Invoke();
    }
    
    private void OnTriggerStay(Collider other)
    {
        OnWindStay.Invoke();
    }
}
