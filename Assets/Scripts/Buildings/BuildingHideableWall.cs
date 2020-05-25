using EndGame.Test.Events;
using EndGame.Test.Triggers;
using System;
using UnityEngine;

namespace EndGame.Test.Buildings
{
    public class BuildingHideableWall : MonoBehaviour
    {
        private Action<IEventArgs> OnTriggerEnteredListener;
        private Action<IEventArgs> OnTriggerExitedListener;

        [SerializeField]
        private SpriteRenderer rendererComponent = null;
        [SerializeField]
        private ActionTrigger hideTrigger = null;

        private void Awake()
        {
            rendererComponent = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            ActivateWallSprite(true);

            OnTriggerEnteredListener = (args) => OnTriggerEntered((OnTriggerEntered)args);
            OnTriggerExitedListener = (args) => OnTriggerExited((OnTriggerExited)args);

            EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_ENTERED, OnTriggerEnteredListener);
            EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_EXITED, OnTriggerExitedListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_ENTERED, OnTriggerEnteredListener);
            EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_EXITED, OnTriggerExitedListener);
        }

        /// <summary>
        /// Checks if the player entered in order to hide the sprite.
        /// </summary>
        /// <param name="_args">On trigger entered args.</param>
        private void OnTriggerEntered(OnTriggerEntered _args)
        {
            if (hideTrigger == _args.trigger)
            {
                // Only hide the sprite if the playerr enters.
                if (_args.actor.CompareTag("Player"))
                {
                    OnHideTriggerEntered();
                }
            }
        }


        /// <summary>
        /// Checks if the player exited the trigger in order to show the sprite.
        /// </summary>
        /// <param name="_args">On trigger exited args.</param>
        private void OnTriggerExited(OnTriggerExited _args)
        {
            if (hideTrigger == _args.trigger)
            {
                if (_args.actor.CompareTag("Player"))
                {
                    OnHideTriggerExited();
                }
            }
        }

        private void OnHideTriggerEntered()
        {
            Debug.Log("Entered building");
            ActivateWallSprite(false);
        }

        private void ActivateWallSprite(bool _activate)
        {
            rendererComponent.enabled = _activate;
        }

        private void OnHideTriggerExited()
        {
            Debug.Log("Exited building");
            ActivateWallSprite(true);
        }
    }
}
