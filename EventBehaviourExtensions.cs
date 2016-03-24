using UnityEngine;
using System.Collections.Generic;

namespace Events
{
    public static class EventBehaviourExtensions
    {
        public static void On<T>(this Component component, DelegateEvent<T> listener) where T : Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.On(listener);
        }

        public static void On<T>(this Component component, DelegateEvent<T> listener, Component to) where T : Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.On(listener, to.GetOrAddComponent<EventBehaviour>().Listener);
        }

        public static void Off<T>(this Component component, DelegateEvent<T> listener) where T : Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.Off(listener);
        }

        public static void Off<T>(this Component component, DelegateEvent<T> listener, Component to) where T : Event
        {
            component.GetOrAddComponent<EventBehaviour>().Listener.Off(listener, to.GetOrAddComponent<EventBehaviour>().Listener);
        }

        private static List<EventBehaviour> list = new List<EventBehaviour>();
        
        public static void Trigger<T>(this Component component, T evt) where T : Event
        {
            if (evt != null)
            {
                list.Clear();
                component.GetComponentsInParent<EventBehaviour>(true, list);
                foreach (var bhv in list)
                {
                    bhv.Listener.Emit(evt);
                    if (evt.stopPropagation)
                    {
                        break;
                    }
                }

                EventBus.Instance.Emit(evt);
            }
        }
        
        public static void Trigger<T>(this GameObject gameObject, T evt) where T : Event
        {
            gameObject.transform.Trigger(evt);
        }
    }
}