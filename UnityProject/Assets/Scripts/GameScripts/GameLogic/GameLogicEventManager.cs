using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameLogicEventManager : MonoBehaviour 
    {
        private Dictionary<Type, Dictionary<GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>> _gameLogicEvents;

        void Awake()
        {
            InitializeFields();
            InitializeGameLogicEvents();
        }

        public void TriggerGameLogicEvent(GameLogicEvent gameLogicEvent, params object[] args)
        {
            foreach (var value in _gameLogicEvents.Values)
            {
                if (value.ContainsKey(gameLogicEvent))
                {
                    foreach (var pair in value[gameLogicEvent])
                    {
                        pair.Value.ForEach(m => m.Invoke(pair.Key, args));
                    }
                }
            }
        }

        public void TriggerGameLogicEvent<T>(GameLogicEvent gameLogicEvent, params object[] args) where T : GameLogic
        {
            foreach (var typeDictPair in _gameLogicEvents)
            {
                if ((typeof(T) == (typeDictPair.Key) || typeDictPair.Key.IsSubclassOf(typeof(T))) && typeDictPair.Value.ContainsKey(gameLogicEvent))
                {
                    foreach (var gameLogicMethodsPair in typeDictPair.Value[gameLogicEvent])
                    {
                        gameLogicMethodsPair.Value.ForEach(m => m.Invoke(gameLogicMethodsPair.Key, args));
                    }
                }
            }
        }

        public void TriggerGameLogicEvent(GameLogic gameLogic, GameLogicEvent gameLogicEvent, params object[] args)
        {
            if (ContainGameLogicEvent(gameLogic, gameLogicEvent))
            {
                foreach (var m in _gameLogicEvents[gameLogic.GetType()][gameLogicEvent][gameLogic])
                {
                    m.Invoke(gameLogic, args);
                }
            }
        }

        private bool ContainGameLogicEvent(GameLogic gameLogic, GameLogicEvent gameLogicEvent)
        {
            return _gameLogicEvents.ContainsKey(gameLogic.GetType()) && _gameLogicEvents[gameLogic.GetType()].ContainsKey(gameLogicEvent) && _gameLogicEvents[gameLogic.GetType()][gameLogicEvent].ContainsKey(gameLogic);
        }

        private void InitializeFields()
        {
            _gameLogicEvents = new Dictionary<Type, Dictionary<GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>>();
        }

        private void InitializeGameLogicEvents()
        {
            foreach (var gameLogic in GetComponents<GameLogic>())
            {
                gameLogic.GameLogicEventManager = this;
                AddGameLogicEvents(gameLogic);
            }
        }

        private void AddGameLogicEvents(GameLogic gameLogic)
        {
            gameLogic.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    GameLogicEventAttribute gameLogicEvent = Attribute.GetCustomAttribute(m, typeof(GameLogicEventAttribute)) as GameLogicEventAttribute;
                    if (gameLogicEvent != null)
                    {
                        Type gameLogicType = gameLogic.GetType();
                        if (!_gameLogicEvents.ContainsKey(gameLogicType))
                        {
                            _gameLogicEvents.Add(gameLogicType, new Dictionary<GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>());
                        }
                        if (!_gameLogicEvents[gameLogicType].ContainsKey(gameLogicEvent.Event))
                        {
                            _gameLogicEvents[gameLogicType].Add(gameLogicEvent.Event, new Dictionary<GameLogic, List<MethodInfo>>());
                        }
                        if (!_gameLogicEvents[gameLogicType][gameLogicEvent.Event].ContainsKey(gameLogic))
                        {
                            _gameLogicEvents[gameLogicType][gameLogicEvent.Event].Add(gameLogic, new List<MethodInfo>());
                        }
                        _gameLogicEvents[gameLogicType][gameLogicEvent.Event][gameLogic].Add(m);
                    }
                });
        }
    }
}
