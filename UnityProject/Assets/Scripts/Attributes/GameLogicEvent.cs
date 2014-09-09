using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GameLogicEvent : Attribute
    {
        public Constants.GameLogicEvent Event { get; private set; }

        public GameLogicEvent(Constants.GameLogicEvent gameLogicEvent)
        {
            Event = gameLogicEvent;
        }
    }
}
