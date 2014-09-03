using System;
using Assets.Scripts.Constants;

namespace Assets.Scripts.Attrubutes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class GameLogicEvent : Attribute
    {
        public GameLogicEventConstants.GameLogicEvent Event { get; private set; }

        public GameLogicEvent(GameLogicEventConstants.GameLogicEvent gameLogicEvent)
        {
            Event = gameLogicEvent;
        }
    }
}
