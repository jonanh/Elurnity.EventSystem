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

        private void OnDestroy()
        {
            this.Listener.RemoveAll();
        }
    }
}
