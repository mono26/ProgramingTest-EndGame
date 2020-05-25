using EndGame.Test.Events;
using EndGame.Test.Game;
using EndGame.Test.UI;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class PlayerView : ActorView
    {
        [SerializeField]
        private TouchJoystick movementJoystick = null;
        [SerializeField]
        private TouchJoystick aimJoystick = null;
        [SerializeField]
        private Camera playerCamera = null;
        [SerializeField]
        private bool forceMobileInput = false;

        protected override void Start()
        {
            base.Start();
#if UNITY_ANDROID
            forceMobileInput = true;
#endif
        }

        private void Update()
        {
            CatchMovementInput();
            // Weapon input.
            CatchAimInput();
            CatchShootInput();
        }

        private void CatchMovementInput()
        {
            // Get the horizontal and vertical input.
            float horizontalInput = 0.0f;
            float verticalInput = 0.0f;
            if (forceMobileInput)
            {
                Vector2 virtualjoystickValue = movementJoystick.GetJoystickValue;
                horizontalInput = virtualjoystickValue.x;
                verticalInput = virtualjoystickValue.y;
            }
            else
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }

            // Only send a movement command when there is movment input..
            if (!horizontalInput.Equals(0.0f) || !verticalInput.Equals(0.0f))
            {
                // Vertical input is mapped out to the z axis.
                Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);

                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                    command = ActorCommands.Move,
                    value = inputVector
                };

                EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }
        }

        private void CatchAimInput()
        {
            Vector3 aimDirection;
            if (forceMobileInput)
            {
                if (aimJoystick.GetIsDragged)
                {
                    aimDirection = new Vector3(aimJoystick.GetJoystickValue.x, 0, aimJoystick.GetJoystickValue.y);

                    if (aimDirection.x != 0.0f || aimDirection.y != 0.0f)
                    {
                        OnActorCommandReceiveEventArgs aimArgs = new OnActorCommandReceiveEventArgs()
                        {
                            baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                            command = ActorCommands.Aim,
                            value = aimDirection
                        };

                        EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, aimArgs);

                        // Also shoot when the player aims with the joystick.
                        OnActorCommandReceiveEventArgs shootArgs = new OnActorCommandReceiveEventArgs()
                        {
                            baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                            command = ActorCommands.Shoot,
                            // Means the shoot button is pressed.
                            value = 1.0f
                        };

                        EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, shootArgs);
                    }
                }
            }
            else
            {
                Vector2 positionOnScreen = playerCamera.WorldToScreenPoint(GetOwner.transform.position);
                Vector2 mouseOnScreen = Input.mousePosition;

                aimDirection = mouseOnScreen - positionOnScreen;
                aimDirection.z = aimDirection.y;
                aimDirection.y = 0;

                if (aimDirection.x != 0.0f || aimDirection.y != 0.0f)
                {
                    OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                    {
                        baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                        command = ActorCommands.Aim,
                        // Means the shoot button is pressed.
                        value = aimDirection
                    };

                    EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
                }
            }
        }

        private void CatchShootInput()
        {
            if (forceMobileInput)
            {
                if (aimJoystick.GetIsUp)
                {
                    if (aimJoystick.GetIsTapped)
                    {
                        OnActorCommandReceiveEventArgs aimArgs = new OnActorCommandReceiveEventArgs()
                        {
                            baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                            command = ActorCommands.AutoAim
                        };

                        EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, aimArgs);

                        OnActorCommandReceiveEventArgs shootArgs = new OnActorCommandReceiveEventArgs()
                        {
                            baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                            command = ActorCommands.Shoot,
                            // Means the shoot button is pressed.
                            value = 1.0f
                        };

                        EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, shootArgs);
                    }
                    else
                    {
                        OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                        {
                            baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                            command = ActorCommands.Shoot,
                            // Means the shoot button has been released.
                            value = 0.0f
                        };

                        EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                    {
                        baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                        command = ActorCommands.Shoot,
                        // Means the shoot button is pressed.
                        value = 1.0f
                    };

                    EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                    {
                        baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                        command = ActorCommands.Shoot,
                        // Means the shoot button has been released.
                        value = 0.0f
                    };

                    EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
                }
            }
        }

        /// <summary>
        /// Sends a player death event when the player health reeaches zero.
        /// </summary>
        /// <param name="_args">Player death args.</param>
        protected override void OnActorDeath(OnActorEventEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                OnPlayerDeathEventArgs args = new OnPlayerDeathEventArgs()
                {

                };

                EventController.QueueEvent(GameEvents.PLAYER_DEATH, args);
            }
        }
    }
}
