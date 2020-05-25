using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Rotator : ActorComponent
    {
        [SerializeField]
        private Detector detectorComponent;

        protected virtual void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnActorCommandReceive((OnActorCommandReceiveEventArgs)args));
        }

        protected virtual void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnActorCommandReceive((OnActorCommandReceiveEventArgs)args));
        }


        /// <summary>
        /// Rotates the actor towards the direction of fire.
        /// </summary>
        /// <param name="_args"></param>
        protected void OnActorFireWeapon(OnActorFireWeapon _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                RotateTowardsTargetDirection(_args.aimDirection);
            }
        }

        /// <summary>
        /// Rotates the actor towards a direction.
        /// </summary>
        /// <param name="_nextDirection"></param>
        private void RotateTowardsTargetDirection(Vector3 _nextDirection)
        {
            GetOwner.transform.LookAt(GetOwner.transform.position + _nextDirection);
        }

        /// <summary>
        /// Rotates the actor when the actor is aiming or auto aiming.
        /// </summary>
        /// <param name="_args"></param>
        private void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                if (_args.command.Equals(ActorCommands.Aim))
                {
                    //Debug.Log("Rotating towards: " + (Vector3)_args.value);

                    RotateTowardsTargetDirection((Vector3)_args.value);
                }
                else if (_args.command.Equals(ActorCommands.AutoAim))
                {
                    // TODO get targeted current target direction.
                    Vector3 targetDirection = detectorComponent.GetTargetDirection;

                    Debug.Log("auto aiming");

                    RotateTowardsTargetDirection(targetDirection);
                }
            }
        }
    }
}
