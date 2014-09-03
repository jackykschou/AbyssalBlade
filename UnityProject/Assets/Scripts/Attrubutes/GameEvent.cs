using System;
using Assets.Scripts.Constants;

namespace Assets.Scripts.Attrubutes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class GameEvent : Attribute
    {
        public GameEventConstants.GameEvent Event{get; private set;}

        public GameEvent(GameEventConstants.GameEvent gameEvent)
        {
            Event = gameEvent;
        }
    }
}
