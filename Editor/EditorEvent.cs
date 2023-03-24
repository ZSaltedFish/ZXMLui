using UnityEngine;

namespace ZKnight.ZXMLui
{
    public class EditorEvent
    {
        public Event Event;

        public EditorEvent(Event e)
        {
            Event = e;
        }

        public void Use()
        {
            Event.Use();
        }

        public bool IsUsed()
        {
            return Event.type == EventType.Used;
        }
    }
}
