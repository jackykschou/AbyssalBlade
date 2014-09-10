using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ComponentEvent : Attribute
    {
        public Constants.ComponentEvent Event { get; private set; }

        public ComponentEvent(Constants.ComponentEvent componentEvent)
        {
            Event = componentEvent;
        }
    }
}
