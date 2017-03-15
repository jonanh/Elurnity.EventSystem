using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Elurnity.EventSystem
{
    public class ListenerDictionary<T> : IEnumerable<KeyValuePair<T, Delegate>>
    {
        internal Dictionary<T, Delegate> dictionary = new Dictionary<T, Delegate>();

        public void Add<U>(T key, Action<U> listener) where U : struct, Event
        {
            Delegate previousListener;
            if (dictionary.TryGetValue(key, out previousListener))
            {
                dictionary[key] = (Action<U>)previousListener + listener;
            }
            else
            {
                dictionary[key] = listener;
            }
        }

        public void Remove<U>(T key, Action<U> listener) where U : struct, Event
        {
            Delegate previousListener;
            if (dictionary.TryGetValue(key, out previousListener))
            {
                var result = (Action<U>)previousListener - listener;
                if (result == null)
                {
                    dictionary.Remove(key);
                }
                else
                {
                    dictionary[key] = result;
                }
            }
        }

        internal bool TryGetValue(T key, out Delegate listener)
        {
            return dictionary.TryGetValue(key, out listener);
        }

        internal void Clear()
        {
            dictionary.Clear();
        }

        internal void Remove(T key, Delegate @delegate)
        {
            Delegate listener;
            if (dictionary.TryGetValue(key, out listener))
            {
                dictionary[key] = Delegate.RemoveAll(listener, @delegate);
            }
        }

        public IEnumerator<KeyValuePair<T, Delegate>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}