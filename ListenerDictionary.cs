using System;
using System.Linq;
using System.Collections.Generic;

namespace Events
{
    public class ListenerDictionary<T> : Dictionary<T, Delegate>
    {
        public void Add<U>(T key, DelegateEvent<U> listener) where U : Event
        {
            Delegate previousListener;
            if (TryGetValue(key, out previousListener))
            {
                this[key] = (DelegateEvent<U>)previousListener + listener;
            }
            else
            {
                this[key] = listener;
            }
        }

        public void Remove<U>(T key, DelegateEvent<U> listener) where U : Event
        {
            Delegate previousListener;
            if (TryGetValue(key, out previousListener))
            {
                var result = (DelegateEvent<U>)previousListener - listener;
                if (result == null)
                {
                    Remove(key);
                }
                else
                {
                    this[key] = result;
                }
            }
        }

        public void Remove(T key, Delegate @delegate)
        {
            Delegate listener;
            if (TryGetValue(key, out listener))
            {
                this[key] = Delegate.RemoveAll(listener, @delegate);
            }
        }
    }
}