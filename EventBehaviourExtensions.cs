using UnityEngine;
using System.Collections.Generic;

namespace Events
{
    public static class EventBehaviourExtensions
    {
        public static EventBehaviour events(this Component component)
        {
            EventBehaviour bhv = component.GetComponent<EventBehaviour>();
            if (bhv == null)
            {
                bhv = component.gameObject.AddComponent<EventBehaviour>();
            }
            return bhv;
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
                    bhv.Listener.Trigger(evt);
                    if (evt.stopPropagation)
                    {
                        break;
                    }
                }

                EventBus.Instance.Trigger(evt);
            }
        }
        
        public static void Trigger<T>(this GameObject gameObject, T evt) where T : Event
        {
            gameObject.transform.Trigger(evt);
        }
    }
}