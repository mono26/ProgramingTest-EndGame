﻿using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Movement : ActorComponent
    {
        public event Action OnMovement;
        public event Action OnStandingStill;

        [SerializeField]
        private Rigidbody bodyComponent = null;
        [SerializeField]
        private float movementSpeed = 3.0f;

        [SerializeField]
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

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            // Catch component references.
            bodyComponent = _actor.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnActorCommandReceive((OnActorCommandReceiveEventArgs)args));
        }

        private void FixedUpdate()
        {
            if (!targetDirection.Equals(Vector3.zero))
            {
                MoveTowardsDirection(targetDirection);

                targetDirection = Vector3.zero;
            }
            else
            {
                OnStandingStill?.Invoke();
            }
        }

        private void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                if (_args.command.Equals(ActorCommands.Move))
                {
                    SetTargetDirection = (Vector3)_args.value;
                }
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
}
