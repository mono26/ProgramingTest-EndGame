using EndGame.Test.Actors;
using UnityEngine;

public class PlayerDetector : Detector
{
    //public override Actor GetCurrenTarget { get => currentTarget; }
    public override Vector3 GetTargetDirection 
    { 
        get
        {
            Vector3 targetDirection = Vector3.zero;
            if (nearTargets != null && nearTargets.Count > 0)
            {
                targetDirection = GetNearesTargetDirection();
            }
            else
            {
                targetDirection = GetOwner.transform.forward;
            }

            return targetDirection;
        }
    }

    private Vector3 GetNearesTargetDirection()
    {
        Vector3 targetDirection = Vector3.zero;
        float nearestSqrDistance = float.MaxValue;
        Vector3 directionVector;
        foreach (Actor target in nearTargets)
        {
            directionVector = target.transform.position - GetOwner.transform.position;
            directionVector.y = GetOwner.transform.position.y;
            if (directionVector.sqrMagnitude < nearestSqrDistance)
            {
                nearestSqrDistance = directionVector.sqrMagnitude;
                targetDirection = directionVector.normalized;
            }
        }

        return targetDirection;
    }
}
