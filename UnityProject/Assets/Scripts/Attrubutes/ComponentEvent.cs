using System;
using Assets.Scripts.Constants;

namespace Assets.Scripts.Attrubutes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class ComponentEvent : Attribute
    {
        public ComponentEventConstants.ComponentEvent Event { get; private set; }

        public ComponentEvent(ComponentEventConstants.ComponentEvent componentEvent)
        {
            Event = componentEvent;
        }
    }
}
