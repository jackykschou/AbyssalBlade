﻿using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GameEvent : Attribute
    {
        public Constants.GameEvent Event{get; private set;}

        public GameEvent(Constants.GameEvent gameEvent)
        {
            Event = gameEvent;
        }
    }
}
