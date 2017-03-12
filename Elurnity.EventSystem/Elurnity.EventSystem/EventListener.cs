using System;

namespace Elurnity.EventSystem
{
    public sealed class EventListener
    {
        internal static event Action<Event> eventDebugger;

        internal ListenerDictionary<Type> listening = new ListenerDictionary<Type>();
        internal ListenerDictionary<Tuple<Type, EventListener>> listeningTo = new ListenerDictionary<Tuple<Type, EventListener>>();

        public void On<T>(Action<T> listener) where T : struct, Event
        {
            listening.Add(typeof(T), listener);
        }

        public void On<T>(Action<T> listener, EventListener to) where T : struct, Event
        {
            to.On<T>(listener);
            var key = new Tuple<Type, EventListener>(typeof(T), to);
            listeningTo.Add(key, listener);
        }

        public void Off<T>(Action<T> listener) where T : struct, Event
        {
            listening.Remove(typeof(T), listener);
        }

        public void Off<T>(Action<T> listener, EventListener to) where T : struct, Event
        {
            to.Off<T>(listener);
            var key = new Tuple<Type, EventListener>(typeof(T), to);
            listeningTo.Remove(key, listener);
        }

        public void Emit<T>(T evt) where T : struct, Event
        {
            Delegate listener;
            if (listening.TryGetValue(typeof(T), out listener))
            {
                ((Action<T>)listener)(evt);
            }

            if (eventDebugger != null)
            {
                eventDebugger(evt);
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
