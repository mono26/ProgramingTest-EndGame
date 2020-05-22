using EndGame.Test.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private void Awake()
    {
        ActorComponent[] components = GetComponents<ActorComponent>();

        foreach (ActorComponent component in components)
        {
            component.OnAwake(this);
        }
    }
}
