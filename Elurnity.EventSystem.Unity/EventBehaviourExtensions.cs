using UnityEngine;
using System;
using System.Collections.Generic;

namespace Elurnity.EventSystem
{
    public static class EventBehaviourExtensions
    {
        public static void On<T>(this Component component, Action<T> listener) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.On(listener);
        }

        public static void On<T>(this Component component, Action<T> listener, Component to) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.On(listener, to.GetOrAddComponent<EventBehaviour>().Listener);
        }

        public static void On<T>(this Component component, Action<T> listener, EventListener to) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.On(listener, to);
        }

        public static void Off<T>(this Component component, Action<T> listener) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.Off(listener);
        }

        public static void Off<T>(this Component component, Action<T> listener, Component to) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.Off(listener, to.GetOrAddComponent<EventBehaviour>().Listener);
        }

        public static void Off<T>(this Component component, Action<T> listener, EventListener to) where T : struct, Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.Off(listener, to);
        }

        private static List<EventBehaviour> list = new List<EventBehaviour>();

        public static void Trigger<T>(this Component component, T evt) where T : struct, Event
        {
            list.Clear();
            component.GetComponentsInParent<EventBehaviour>(true, list);
            foreach (var bhv in list)
            {
                bhv.Listener.Emit(evt);
                // TODO: Reimplement it later, probably we will need two types of events :-)
                //if (evt.stopPropagation)
                //{
                //    break;
                //}
            }

            EventBus.Instance.Emit(evt);
        }

        public static void Trigger<T>(this GameObject gameObject, T evt) where T : struct, Event
        {
            gameObject.transform.Trigger(evt);
        }
    }
}