using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace Events
{
    public class EventDebuggerWindow : VexeEditorWindow
    {
        public List<Event> events = new List<Event>();
        private List<Event> capturedEvents = new List<Event>();

        [Show]
        public void Clear()
        {
            events.Clear();
            capturedEvents.Clear();
            Repaint();
        }

        [ShowType(typeof(Event))]
        public Type eventType = typeof(Event);

        [Show]
        public void Filter()
        {
            events.Clear();
            events.AddRange(
                from capturedEvent in capturedEvents
                where filter(capturedEvent)
                select capturedEvent);
            Repaint();
        }

        protected bool filter(Event evt)
        {
            return eventType.IsAssignableFrom(evt.GetType());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventListener.globalListener += OnEvent;
            EditorApplication.playmodeStateChanged += OnPlayModeChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventListener.globalListener -= OnEvent;
            EditorApplication.playmodeStateChanged += OnPlayModeChanged;
        }

        protected void OnEvent(Event evt)
        {
            if (filter(evt))
            {
                events.Add(evt);
            }
            capturedEvents.Add(evt);
            Repaint();
        }

        protected void OnPlayModeChanged()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Clear();
            }
        }

        public static class MenuItems
        {
            [MenuItem("Window/Events/Event debugger")]
            public static void ShowWindow()
            {
                EditorWindow.GetWindow<EventDebuggerWindow>("Event Debugger");
            }
        }
    }
}
