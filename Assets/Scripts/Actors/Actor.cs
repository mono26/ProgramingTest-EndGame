using EndGame.Test.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    /// <summary>
    /// Use to find a midle point inside the actor. Usually is the height of the collider. Best for raycast purpose.
    /// </summary>
    [SerializeField]
    private float actorHeight;

    // public float GetHeight { get => actorHeight; }
    public Vector3 GetCenterOfBodyPosition 
    { 
        get
        {
            Vector3 centerPosition = transform.position;
            centerPosition.y += actorHeight / 2;
            return centerPosition;
        } 
    }

    private void Awake()
    {
        ActorComponent[] components = GetComponents<ActorComponent>();

        foreach (ActorComponent component in components)
        {
            component.OnAwake(this);
        }
    }
}
