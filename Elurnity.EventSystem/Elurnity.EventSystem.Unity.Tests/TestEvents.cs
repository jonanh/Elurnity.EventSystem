
using System.Collections;

namespace Elurnity.EventSystem.Tests
{
    public struct TestEvent : Event
    {
        public string field;
    }

    public class BasicTests
    {
        /*
        public class TestEvents : MonoBehaviour
        {
            public Text text;
            public bool attachToEventBus = false;
            private int message;
            private IEnumerator lastCoroutine;

            public void Add()
            {
                if (attachToEventBus)
                {
                    this.On<TestEvent>(OnEvent, EventBus.Instance);
                }
                else
                {
                    this.On<TestEvent>(OnEvent);
                }
            }

            public void Remove()
            {
                if (attachToEventBus)
                {
                    this.Off<TestEvent>(OnEvent, EventBus.Instance);
                }
                else
                {
                    this.Off<TestEvent>(OnEvent);
                }
            }

            public void Trigger()
            {
                this.Trigger(new TestEvent()
                {
                    field = "Message " + message++ + " from " + gameObject,
                });
            }

            public void OnEvent(TestEvent evt)
            {
                Debug.Log(evt.field + " listened by " + gameObject);

                if (lastCoroutine != null)
                {
                    StopCoroutine(lastCoroutine);
                }
                lastCoroutine = blink();
                StartCoroutine(lastCoroutine);
            }

            public IEnumerator blink()
            {
                text.color = Color.green;
                yield return new WaitForSeconds(0.5f);
                text.color = Color.white;
                lastCoroutine = null;
            }
        }
        */
    }
}
