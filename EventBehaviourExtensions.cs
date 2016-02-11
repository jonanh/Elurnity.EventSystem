using UnityEngine;

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
    }
}