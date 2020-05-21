using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDoor : MonoBehaviour
{
    [SerializeField]
    private Animator doorController;
    [SerializeField]
    private ActionTrigger openCloseTrigger;

    private void Awake()
    {
        doorController.GetComponent<Animator>();
    }

    private void Start()
    {
        openCloseTrigger.OnTriggerEntered += OnTriggerEntered;
        openCloseTrigger.OnTriggerExited += OnTriggerExited;
    }

    private void OnDestroy()
    {
        openCloseTrigger.OnTriggerEntered -= OnTriggerEntered;
        openCloseTrigger.OnTriggerExited -= OnTriggerExited;
    }

    private void OnTriggerEntered()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        doorController.SetTrigger("Open");
    }

    private void OnTriggerExited()
    {
        CloseDoor();
    }

    private void CloseDoor()
    {
        doorController.SetTrigger("Close");
    }
}
