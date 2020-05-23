using EndGame.Test.Actors;
using UnityEngine;

public class PlayerDetector : Detector
{
    public override Actor GetCurrenTarget { get => currentTarget; }
    public override Vector3 GetTargetDirection { get => GetOwner.transform.forward; }
}
