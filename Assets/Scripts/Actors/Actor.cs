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
    [SerializeField]
    private float actorWidth;

    public float GetHeight { get => actorHeight; }

    private void Awake()
    {
        ActorComponent[] components = GetComponents<ActorComponent>();

        foreach (ActorComponent component in components)
        {
            component.OnAwake(this);
        }
    }
}
