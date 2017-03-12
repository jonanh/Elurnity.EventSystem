using UnityEngine;
using System;

namespace Elurnity.EventSystem
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
