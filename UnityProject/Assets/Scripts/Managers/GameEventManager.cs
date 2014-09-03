using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Constants;
using Assets.Scripts.Exceptions;
using Assets.Scripts.GameScripts;
using Assets.Scripts.GameScripts.Components;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameEventManager
    {
        public static GameEventManager Instance { get { return _instance; } }

        private Dictionary<GameEventConstants.GameEvent, Dictionary<System.Object, List<MethodInfo>>> _gameEvents;

        private static readonly GameEventManager _instance = new GameEventManager();

        GameEventManager()
        {
            _gameEvents = new Dictionary<GameEventConstants.GameEvent, Dictionary<object, List<MethodInfo>>>();
        }

        public void TriggerGameEvent(GameEventConstants.GameEvent gameEvent, params System.Object[] args)
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

        public void SubscribeGameEvent(System.Object subscriber, GameEventConstants.GameEvent gameEvent, MethodInfo info)
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

        public void UnsubscribeGameEvent(System.Object subscriber, GameEventConstants.GameEvent gameEvent)
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
