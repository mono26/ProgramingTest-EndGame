using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public event Action OnTriggerEntered;
    public event Action OnTriggerExited;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke();
    }
}
