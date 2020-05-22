using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public event Action OnMovement;
    public event Action OnStandingStill;

    [SerializeField]
    private Rigidbody bodyComponent = null;
    [SerializeField]
    private float movementSpeed = 3.0f;

    private Vector3 targetDirection = Vector3.zero;

    public float GetMovementSpeed { get => movementSpeed; }
    public Vector3 SetTargetDirection { set => targetDirection = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 startPosition = transform.position;
        startPosition.y += 0.5f;
        Vector3 targetPosition = startPosition + targetDirection;

        Gizmos.DrawLine(startPosition, targetPosition);
    }

    private void Awake()
    {
        // Catch component references.
        bodyComponent = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get the horizontal and vertical input.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Only calculate the target position if there's movement input.
        if (!horizontalInput.Equals(0.0f) || !verticalInput.Equals(0.0f))
        {
            // Vertical input is mapped out to the z axis.
            Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);

            SetTargetDirection = inputVector;
        }
        //else
        //{
        //    OnStandingStill?.Invoke();
        //}
    }

    private void FixedUpdate()
    {
        // Get the horizontal and vertical input.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Only calculate the target position if there's movement input.
        if (!targetDirection.Equals(Vector3.zero))
        {
            MoveTowardsDirection(targetDirection);
        }
        else
        {
            OnStandingStill?.Invoke();
        }
    }


    private void MoveTowardsDirection(Vector3 _directionVector)
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = currentPosition + _directionVector * movementSpeed * Time.fixedDeltaTime;

        MoveTowardsTargetPosition(nextPosition);
    }

    private void MoveTowardsTargetPosition(Vector3 _nextPosition)
    {
        bodyComponent.MovePosition(_nextPosition);

        RotateTowardsTargetPosition(_nextPosition);

        OnMovement?.Invoke();
    }

    private void RotateTowardsTargetPosition(Vector3 _nextPosition)
    {
        transform.LookAt(_nextPosition);
    }
}
