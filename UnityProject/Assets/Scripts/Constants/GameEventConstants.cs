using System;
using System.Collections.Generic;
using Assets.Scripts.Exceptions;

namespace Assets.Scripts.Constants
{
    public static class GameEventConstants
    {
        public delegate void ExampleEvent();

        public enum GameEvent
        {
            ExampleEvent
        };

        public static Type GetEventType(GameEvent gameEvent)
        {
            if (EventTypeMapping.ContainsKey(gameEvent))
            {
                return EventTypeMapping[gameEvent];
            }
            else
            {
                throw new UndefinedGameEventException();
            }
        }

        private static readonly Dictionary<GameEvent, Type> EventTypeMapping = new Dictionary<GameEvent, Type>() 
        {
            {GameEvent.ExampleEvent, typeof(ExampleEvent)}
        };
    }
}
