using UnityEngine;

public class Actor : MonoBehaviour
{
    /// <summary>
    /// Use to find a midle point inside the actor. Usually is the height of the collider. Best for raycast purpose.
    /// </summary>
    [SerializeField]
    private float actorHeight;

    /// <summary>
    /// Gets a position in the center of the actor bounds.
    /// </summary>
    public Vector3 GetCenterOfBodyPosition 
    { 
        get
        {
            Vector3 centerPosition = transform.position;
            centerPosition.y += actorHeight / 2;
            return centerPosition;
        } 
    }
}
