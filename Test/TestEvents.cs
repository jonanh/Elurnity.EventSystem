using UnityEngine;
using System.Collections;
using Events;

namespace Tests
{
    public class TestEvent : Events.Event
    {
        public string field = "Test";
    }

    public class TestEvents : MonoBehaviour
    {
        private int message;

        public void Add()
        {
            this.events().Add<TestEvent>(OnEvent);
        }

        public void Remove()
        {
            this.events().Remove<TestEvent>(OnEvent);
        }

        public void Trigger()
        {
            this.events().Trigger(new TestEvent()
            {
                field = "Message " + message++ + " from " + gameObject,
            });
        }

        public void OnEvent(TestEvent evt)
        {
            Debug.Log(evt.field + " listened by " + gameObject);
        }
    }
}