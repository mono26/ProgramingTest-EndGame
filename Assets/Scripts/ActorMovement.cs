using System;
using UnityEngine;

public class ActorMovement : MonoBehaviour
{
    public event Action OnMovement;
    public event Action OnStandingStill;

    [SerializeField]
    private Rigidbody bodyComponent = null;
    [SerializeField]
    private float movementSpeed = 3.0f;

    private void Awake()
    {
        // Catch component references.
        bodyComponent = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get the horizontal and vertical input.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Only calculate the target position if there's movement input.
        if (!horizontalInput.Equals(0.0f) || !verticalInput.Equals(0.0f))
        {
            Vector3 currentPosition = transform.position;
            // Vertical input is mapped out to the z axis.
            Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
            Vector3 nextPosition = currentPosition + inputVector * movementSpeed * Time.fixedDeltaTime;

            bodyComponent.MovePosition(nextPosition);

            RotateTowardsTargetPosition(nextPosition);

            OnMovement?.Invoke();
        }
        else
        {
            OnStandingStill?.Invoke();
        }
    }

    private void RotateTowardsTargetPosition(Vector3 _nextPosition)
    {
        transform.LookAt(_nextPosition);
    }
}
