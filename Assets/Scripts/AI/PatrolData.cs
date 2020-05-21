using UnityEngine;

public class PatrolData : AIStateData
{
    [SerializeField]
    private Vector3[] patrolPoints = new Vector3[0];

    private int currentPatrolPoint = 0;

    /// <summary>
    /// Gets the current patrol position.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPatrolPosition() => patrolPoints[currentPatrolPoint];

    /// <summary>
    /// Advances de current patrol point to the next one.
    /// </summary>
    public void OnPatrolPointReached()
    {
        currentPatrolPoint++;
        currentPatrolPoint = currentPatrolPoint % patrolPoints.Length;
    }
}
