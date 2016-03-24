using System;
using System.Linq;
using System.Collections.Generic;

namespace Events
{
    public sealed class EventListener
    {
        private ListenerDictionary<Type> listening = new ListenerDictionary<Type>();
        private ListenerDictionary<Tuple<Type, EventListener>> listeningTo = new ListenerDictionary<Tuple<Type, EventListener>>();

        public void On<T>(DelegateEvent<T> listener) where T : Event
        {
            listening.Add(typeof(T), listener);
        }

        public void On<T>(DelegateEvent<T> listener, EventListener to) where T : Event
        {
            to.On<T>(listener);
            var key = new Tuple<Type, EventListener>(typeof(T), to);
            listeningTo.Add(key, listener);
        }

        public void Off<T>(DelegateEvent<T> listener) where T : Event
        {
            listening.Remove(typeof(T), listener);
        }

        public void Off<T>(DelegateEvent<T> listener, EventListener to) where T : Event
        {
            to.Off<T>(listener);
            var key = new Tuple<Type, EventListener>(typeof(T), to);
            listeningTo.Remove(key, listener);
        }

        public void Emit<T>(T evt) where T : Event
        {
            Delegate listener;
            if (listening.TryGetValue(typeof(T), out listener))
            {
                ((DelegateEvent<T>)listener)(evt);
            }
        }

        public void RemoveAll()
        {
            listening.Clear();

            foreach (var entry in listeningTo)
            {
                var type = entry.Key.Item1;
                var to = entry.Key.Item2;
                to.listening.Remove(type, entry.Value);
            }
        }
    }
}