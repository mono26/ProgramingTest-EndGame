using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHideableWall : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer componentSprite;
    [SerializeField]
    private BuildingTrigger hideTrigger;

    private void Awake()
    {
        hideTrigger.OnTriggerEntered += OnBuildingEntered;
        hideTrigger.OnTriggerExited += OnBuildingExited;
    }

    private void OnDestroy()
    {
        hideTrigger.OnTriggerEntered -= OnBuildingEntered;
        hideTrigger.OnTriggerExited -= OnBuildingExited;
    }

    private void OnBuildingEntered()
    {
        Debug.Log("Entered building");
        ActivateWallSprite(false);
    }

    private void ActivateWallSprite(bool _activate)
    {
        componentSprite.enabled = _activate;
    }

    private void OnBuildingExited()
    {
        Debug.Log("Exited building");
        ActivateWallSprite(true);
    }
}
