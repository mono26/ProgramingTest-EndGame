using System;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Events
{
    public class EventController : Singleton<EventController>
    {
        private Dictionary<string, List<Action<IEventArgs>>> eventsMap = new Dictionary<string, List<Action<IEventArgs>>>();

        // Multiple queues are used to prevent recursive event triggering.
        private int activeQueue = 0;
        private Queue<KeyValuePair<string, IEventArgs>>[] queues = null;

        #region Unity functions
        protected override void Awake()
        {
            base.Awake();

            Init();
        }

        private void Update()
        {
            int eventsCount = queues[activeQueue].Count;
            if (queues[activeQueue] != null && eventsCount > 0)
            {
                for (int i = 0; i < eventsCount; i++)
                {
                    KeyValuePair<string, IEventArgs> tempEvent = queues[activeQueue].Dequeue();
                    TriggerEvent(tempEvent.Key, tempEvent.Value);
                }
            }

            activeQueue++;
            activeQueue = activeQueue % 2;
        }
        #endregion

        /// <summary>
        /// Initialized the event controller to a clean state.
        /// </summary>
        private void Init()
        {
            activeQueue = 0;
            queues = new Queue<KeyValuePair<string, IEventArgs>>[2] {
                new Queue<KeyValuePair<string, IEventArgs>>(0),
                new Queue<KeyValuePair<string, IEventArgs>>(0)
            };
        }

        /// <summary>
        /// Triggers a specific event calling all the listeners in te listeners list.
        /// </summary>
        /// <param name="_eventId">Id of the event to trigger.</param>
        /// <param name="_args">Event data.</param>
        private void TriggerEvent(string _eventId, IEventArgs _args)
        {
            if (eventsMap.ContainsKey(_eventId))
            {
                foreach (Action<IEventArgs> action in eventsMap[_eventId])
                {
                    action?.Invoke(_args);
                }
            }
            else
            {
                // TODO throw trying to fire and event that doesn't exist.
            }
        }

        /// <summary>
        /// Clears the event map.
        /// </summary>
        private void ClearAllEvents()
        {
            eventsMap.Clear();
        }

        /// <summary>
        /// Sibscribes a listener to a specific event.
        /// </summary>
        /// <param name="_eventId">Id of the event to subscribe.</param>
        /// <param name="_action">Action to call when the event triggers.</param>
        public static void SubscribeToEvent(string _eventId, Action<IEventArgs> _action)
        {
            if (GetUniqueInstance.eventsMap.ContainsKey(_eventId))
            {
                if (!GetUniqueInstance.eventsMap[_eventId].Contains(_action))
                {
                    GetUniqueInstance.eventsMap[_eventId].Add(_action);
                }
                else
                {
                    Debug.LogWarning($"Subscribing action that is already subscribed { _action.GetHashCode() }");
                }
            }
            else
            {
                GetUniqueInstance.eventsMap[_eventId] = new List<Action<IEventArgs>>() { _action };
            }
        }

        /// <summary>
        /// Unsubscribes an action from a specific event.
        /// </summary>
        /// <param name="_eventId">Id of the event to unsubscribe from.</param>
        /// <param name="_action">Action to unsubscribe.</param>
        public static void UnSubscribeFromEvent(string _eventId, Action<IEventArgs> _action)
        {
            if (GetUniqueInstance.eventsMap.ContainsKey(_eventId))
            {
                if (GetUniqueInstance.eventsMap[_eventId].Contains(_action))
                {
                    GetUniqueInstance.eventsMap[_eventId].Remove(_action);
                }
                else
                {
                    Debug.LogWarning($"Unsubscribing action that is not subscribed { _action.GetHashCode() }");
                }
            }
            else
            {
                Debug.LogWarning($"Unsubscribing action from event that is not subscribed { _action.GetHashCode() }");
            }
        }

        /// <summary>
        /// Pushes a event into the event queue.
        /// </summary>
        /// <param name="_id">Id of the event to push.</param>
        /// <param name="_args">Event data.</param>
        public static void QueueEvent(string _id, IEventArgs _args)
        {
            // Debug.Log("Pushing event: " + _id);

            int queueIndex = (GetUniqueInstance.activeQueue + 1) % 2;
            GetUniqueInstance.queues[queueIndex].Enqueue(new KeyValuePair<string, IEventArgs>(_id, _args));
        }

        /// <summary>
        /// Pushes a event into the event queue.
        /// </summary>
        /// <param name="_id">Id of the event to push.</param>
        /// <param name="_args">Event data.</param>
        public static void PushEvent(string _id, IEventArgs _args)
        {
            // Debug.Log("Pushing event: " + _id);

            GetUniqueInstance.queues[GetUniqueInstance.activeQueue].Enqueue(new KeyValuePair<string, IEventArgs>(_id, _args));
        }
    }
}
