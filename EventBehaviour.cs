using UnityEngine;
using System;
using System.Collections.Generic;

namespace Events
{
    public sealed class EventBehaviour : MonoBehaviour
    {
        private static Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();
        private Dictionary<Type, Delegate> localEvents = new Dictionary<Type, Delegate>();

        private void Awake()
        {
            this.hideFlags = HideFlags.HideAndDontSave;
        }

        public void Add<T>(DelegateEvent<T> listener) where T : Event
        {
            Delegate previousListener;
            if (events.TryGetValue(typeof(T), out previousListener))
            {
                events[typeof(T)] = (DelegateEvent<T>)previousListener + listener;
            }
            else
            {
                events[typeof(T)] = listener;
            }

            if (localEvents.TryGetValue(typeof(T), out previousListener))
            {
                localEvents[typeof(T)] = (DelegateEvent<T>)previousListener + listener;
            }
            else
            {
                localEvents[typeof(T)] = listener;
            }
        }

        public void Remove<T>(DelegateEvent<T> listener) where T : Event
        {
            Delegate previousListener;
            if (events.TryGetValue(typeof(T), out previousListener))
            {
                var result = (DelegateEvent<T>)previousListener - listener;
                if (result == null)
                {
                    events.Remove(typeof(T));
                }
                else
                {
                    events[typeof(T)] = result;
                }
            }

            if (localEvents.TryGetValue(typeof(T), out previousListener))
            {
                var result = (DelegateEvent<T>)previousListener - listener;
                if (result == null)
                {
                    localEvents.Remove(typeof(T));
                }
                else
                {
                    localEvents[typeof(T)] = result;
                }
            }
        }

        private static List<EventBehaviour> list = new List<EventBehaviour>();

        public void Trigger<T>(T evt) where T : Event
        {
            list.Clear();
            GetComponentsInParent<EventBehaviour>(true, list);
            foreach (var bhv in list)
            {
                bhv.trigger(evt);
                if (evt.stopPropagation)
                {
                    break;
                }
            }
        }

        private void trigger<T>(T evt) where T : Event
        {
            Delegate listener;
            if (events.TryGetValue(typeof(T), out listener))
            {
                ((DelegateEvent<T>)listener)(evt);
            }
        }

        private void OnDestroy()
        {
            foreach (var entry in localEvents)
            {
                Delegate listener;
                if (events.TryGetValue(entry.Key, out listener))
                {
                    events[entry.Key] = Delegate.RemoveAll(listener, entry.Value);
                }
            }
        }
    }
}