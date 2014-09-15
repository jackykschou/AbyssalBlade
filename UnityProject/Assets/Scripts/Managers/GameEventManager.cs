﻿using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Constants;

namespace Assets.Scripts.Managers
{
    public class GameEventManager
    {
        public static GameEventManager Instance { get { return _instance; } }

        private Dictionary<GameEvent, Dictionary<System.Object, List<MethodInfo>>> _gameEvents;

        private static readonly GameEventManager _instance = new GameEventManager();

        GameEventManager()
        {
            _gameEvents = new Dictionary<GameEvent, Dictionary<object, List<MethodInfo>>>();
        }

        public void TriggerGameEvent(System.Object obj, GameEvent gameEvent, params System.Object[] args)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            if (!_gameEvents[gameEvent].ContainsKey(obj))
            {
                return;
            }

            _gameEvents[gameEvent][obj].ForEach(m => m.Invoke(obj, args));
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            foreach (var pair in _gameEvents[gameEvent])
            {
                pair.Value.ForEach(m => m.Invoke(pair.Key, args));
            }
        }

        public void SubscribeGameEvent(System.Object subscriber, GameEvent gameEvent, MethodInfo info)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                _gameEvents.Add(gameEvent, new Dictionary<object, List<MethodInfo>>());
            }
            if (!_gameEvents[gameEvent].ContainsKey(subscriber))
            {
                _gameEvents[gameEvent].Add(subscriber, new List<MethodInfo>());
            }
            _gameEvents[gameEvent][subscriber].Add(info);
        }

        public void UnsubscribeGameEvent(System.Object subscriber, GameEvent gameEvent)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            if (!_gameEvents[gameEvent].ContainsKey(subscriber))
            {
                return;
            }
            _gameEvents[gameEvent].Remove(subscriber);
        }
    }
}
