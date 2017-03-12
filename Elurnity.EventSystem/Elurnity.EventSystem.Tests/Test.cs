using NUnit.Framework;
using System;

namespace Elurnity.EventSystem.Tests
{
    [TestFixture]
    public class Test
    {
        public struct TestEvent : Event
        {
            public string field;
        }

        [Test]
        public void ShouldAddHandlerToListener()
        {
            var listener = new EventListener();

            Action<TestEvent> handler = (TestEvent testEvent) => { };

            listener.On<TestEvent>(handler);

            Assert.IsTrue(listener.listening.dictionary.ContainsKey((typeof(TestEvent))));
        }

        [Test]
        public void ShouldRemoveHandlerFromListener()
        {
            var listener = new EventListener();

            Action<TestEvent> handler = (TestEvent testEvent) => { };

            listener.On<TestEvent>(handler);

            listener.Off<TestEvent>(handler);

            Assert.IsTrue(!listener.listening.dictionary.ContainsKey((typeof(TestEvent))));
        }

        [Test]
        public void ShoulReceiveEmittedEvent()
        {
            var listener = new EventListener();
            var received = 0;

            Action<TestEvent> handler = (TestEvent testEvent) =>
            {
                if (testEvent.field == "0123")
                {
                    received++;
                }
            };

            listener.On<TestEvent>(handler);

            listener.Emit<TestEvent>(new TestEvent()
            {
                field = "0123",
            });

            Assert.AreEqual(1, received);
        }

        [Test]
        public void ShouldAddHandlerToListened()
        {
            var listener = new EventListener();
            var listened = new EventListener();

            Action<TestEvent> handler = (TestEvent testEvent) => { };

            listener.On<TestEvent>(handler, listened);

            Assert.IsTrue(listened.listening.dictionary.ContainsKey((typeof(TestEvent))));

            Assert.IsTrue(listener.listeningTo.dictionary.ContainsKey(Tuple.Create(typeof(TestEvent), listened)));
        }

        [Test]
        public void ShouldRemoveHandlerFromListened()
        {
            var listener = new EventListener();
            var listened = new EventListener();

            Action<TestEvent> handler = (TestEvent testEvent) => { };

            listener.On<TestEvent>(handler, listened);

            listener.Off<TestEvent>(handler, listened);

            Assert.IsTrue(!listened.listening.dictionary.ContainsKey((typeof(TestEvent))));

            Assert.IsTrue(!listener.listeningTo.dictionary.ContainsKey(Tuple.Create(typeof(TestEvent), listened)));
        }

        [Test]
        public void ShouldReceiveEventFromListened()
        {
            var listenerA = new EventListener();
            var listenerB = new EventListener();
            var emitted = false;

            Action<TestEvent> handler = (TestEvent testEvent) =>
            {
                if (testEvent.field == "0123")
                {
                    emitted = true;
                }
            };

            listenerA.On<TestEvent>(handler, listenerB);

            listenerB.Emit<TestEvent>(new TestEvent()
            {
                field = "0123",
            });

            Assert.IsTrue(emitted);
        }
    }
}
