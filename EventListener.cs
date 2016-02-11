using System;
using System.Linq;
using System.Collections.Generic;

namespace Events
{
    public sealed class EventListener
    {
        private Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();

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
        }

        public void Trigger<T>(T evt) where T : Event
        {
            Delegate listener;
            if (events.TryGetValue(typeof(T), out listener))
            {
                ((DelegateEvent<T>)listener)(evt);
            }
        }

        public void RemoveAll()
        {
            RemoveAll(events);
        }
        
        public void RemoveAll(Dictionary<Type, Delegate> events)
        {
            foreach (var entry in events.ToList())
            {
                Delegate listener;
                if (this.events.TryGetValue(entry.Key, out listener))
                {
                    this.events[entry.Key] = Delegate.RemoveAll(listener, entry.Value);
                }
            }
        }
    }
}