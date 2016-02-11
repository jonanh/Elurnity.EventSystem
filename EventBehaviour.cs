using UnityEngine;
using System;

namespace Events
{
    public sealed class EventBehaviour : MonoBehaviour
    {
        public EventListener Listener = new EventListener();

        private void Awake()
        {
            this.hideFlags = HideFlags.HideAndDontSave;
        }

        public void Add<T>(DelegateEvent<T> listener) where T : Event
        {
            this.Listener.Add(listener);
        }

        public void Remove<T>(DelegateEvent<T> listener) where T : Event
        {
            this.Listener.Remove(listener);
        }

        private void OnDestroy()
        {
            this.Listener.RemoveAll();
        }
    }
}
