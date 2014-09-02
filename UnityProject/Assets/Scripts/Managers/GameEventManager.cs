using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Constants;
using Assets.Scripts.Exceptions;
using Assets.Scripts.GameScripts;
using Assets.Scripts.GameScripts.Components;

namespace Assets.Scripts.Managers
{
    public class GameEventManager
    {
        #region event delegates
        private static event GameEventConstants.ExampleEvent ExampleEvent;
        #endregion

        public static GameEventManager Instance { get { return _instance; } }
        private static readonly GameEventManager _instance = new GameEventManager();

        public static void TriggerGameEvent(GameEventConstants.GameEvent gameEvent, params System.Object[] args)
        {
            Delegate eventDelegaet = GetEventDelegate(gameEvent);
            if (eventDelegaet != null)
            {
                eventDelegaet.DynamicInvoke(args);
            }
        }

        public static void SubscribeGameEvent(GameScript gameScript, GameEventConstants.GameEvent gameEvent, Delegate subscriber)
        {
            EventInfo eventInfo = _instance.GetType().GetEvent(GetEventName(gameEvent), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (eventInfo != null)
            {
                eventInfo.AddEventHandler(gameScript, subscriber);
            }
        }

        public static void SubscribeGameEvent(SerializableComponent component, GameEventConstants.GameEvent gameEvent, Delegate subscriber)
        {
            EventInfo eventInfo = _instance.GetType().GetEvent(GetEventName(gameEvent), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (eventInfo != null)
            {
                eventInfo.AddEventHandler(component, subscriber);
            }
        }

        public static void UnsubscribeGameEvent(GameScript gameScript, GameEventConstants.GameEvent gameEvent, Delegate subscriber)
        {
            EventInfo eventInfo = _instance.GetType().GetEvent(GetEventName(gameEvent), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (eventInfo != null)
            {
                eventInfo.RemoveEventHandler(gameScript, subscriber);
            }
        }

        public static void UnsubscribeGameEvent(SerializableComponent component, GameEventConstants.GameEvent gameEvent, Delegate subscriber)
        {
            EventInfo eventInfo = _instance.GetType().GetEvent(GetEventName(gameEvent), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (eventInfo != null)
            {
                eventInfo.RemoveEventHandler(component, subscriber);
            }
        }

        private static String GetEventName(GameEventConstants.GameEvent gameEvent)
        {
            if (EventNameMapping.ContainsKey(gameEvent))
            {
                return EventNameMapping[gameEvent];
            }
            else
            {
                throw new UndefinedGameEventException();
            }
        }

        private static Delegate GetEventDelegate(GameEventConstants.GameEvent gameEvent)
        {
            if (EventDelegateMapping.ContainsKey(gameEvent))
            {
                return EventDelegateMapping[gameEvent];
            }
            else
            {
                throw new UndefinedGameEventException();
            }
        }

        private static readonly Dictionary<GameEventConstants.GameEvent, string> EventNameMapping = new Dictionary<GameEventConstants.GameEvent, string>() 
        {
            {GameEventConstants.GameEvent.ExampleEvent, "ExampleEvent"}
        };

        private static readonly Dictionary<GameEventConstants.GameEvent, Delegate> EventDelegateMapping = new Dictionary<GameEventConstants.GameEvent, Delegate>() 
        {
            {GameEventConstants.GameEvent.ExampleEvent, ExampleEvent}
        };
    }
}
