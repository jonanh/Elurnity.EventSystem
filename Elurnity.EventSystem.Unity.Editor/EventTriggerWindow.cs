using UnityEditor;
using System;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace Events
{
    public class EventTriggerWindow : VexeEditorWindow
    {
        [Show, Inline]
        public Event @event;

        [Show]
        public void Trigger()
        {
            if (Selection.transforms.Length > 0)
            {
                foreach (var element in Selection.transforms)
                {
                    element.Trigger(@event);
                }
            }
            else
            {
                EventBus.Instance.Emit(@event);
            }
        }

        public static class MenuItems
        {
            [MenuItem("Window/Events/Trigger Events")]
            public static void ShowMyWindow()
            {
                EditorWindow.GetWindow<EventTriggerWindow>("Event Trigger");
            }
        }
    }
}
