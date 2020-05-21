using EndGame.Test.AI;
using UnityEngine;

public class Patrol : AIAction
{
    public override void DoAction(AIStateController _controller, AIStateData _data)
    {
        PatrolAction(_controller, (PatrolData)_data);
    }

    private void PatrolAction(AIStateController _controller, PatrolData _data)
    {
        PatrolToTargetPoint(_controller, _data.GetPatrolPosition());
    }

    private void PatrolToTargetPoint(AIStateController _controller, Vector3 _targetPosition)
    {
        Vector3 currentPosition = _controller.GetOwner.transform.position;
        Vector3 directionTowardsPosition = _targetPosition - currentPosition;

        Movement movement = _controller.GetOwner.GetComponent<Movement>();
        movement.SetTargetDirection = directionTowardsPosition;
    }
}
