using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameLogicEventManager : MonoBehaviour 
    {
        private Dictionary<Type, Dictionary<GameLogicEventConstants.GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>> _gameLogicEvents;

        void Awake()
        {
            InitializeFields();
            InitializeGameLogicEvents();
        }

        public void TriggerGameLogicEvent<T>(GameLogicEventConstants.GameLogicEvent gameLogicEvent, params object[] args) where T : GameLogic
        {
            if (ContainGameLogicEvent<T>(gameLogicEvent))
            {
                foreach (KeyValuePair<GameLogic, List<MethodInfo>> pair in _gameLogicEvents[typeof(T)][gameLogicEvent])
                {
                    GameLogic gameLogic = pair.Key;
                    pair.Value.ForEach(m => m.Invoke(gameLogic, args));
                }
            }
        }

        private bool ContainGameLogicEvent<T>(GameLogicEventConstants.GameLogicEvent gameLogicEvent) where T : GameLogic
        {
            return _gameLogicEvents.ContainsKey(typeof(T)) && _gameLogicEvents[typeof(T)].ContainsKey(gameLogicEvent);
        }

        public void TriggerGameLogicEvent(GameLogic gameLogic, GameLogicEventConstants.GameLogicEvent gameLogicEvent, params object[] args)
        {
            if (ContainGameLogicEvent(gameLogic, gameLogicEvent))
            {
                foreach (MethodInfo m in _gameLogicEvents[gameLogic.GetType()][gameLogicEvent][gameLogic])
                {
                    m.Invoke(gameLogic, args);
                }
            }
        }

        private bool ContainGameLogicEvent(GameLogic gameLogic, GameLogicEventConstants.GameLogicEvent gameLogicEvent)
        {
            return _gameLogicEvents.ContainsKey(gameLogic.GetType()) && _gameLogicEvents[gameLogic.GetType()].ContainsKey(gameLogicEvent) && _gameLogicEvents[gameLogic.GetType()][gameLogicEvent].ContainsKey(gameLogic);
        }

        private void InitializeFields()
        {
            _gameLogicEvents = new Dictionary<Type, Dictionary<GameLogicEventConstants.GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>>();
        }

        private void InitializeGameLogicEvents()
        {
            foreach (GameLogic gameLogic in GetComponents<GameLogic>())
            {
                gameLogic.GameLogicEventManager = this;
                AddGameLogicEvents(gameLogic);
            }
        }

        private void AddGameLogicEvents(GameLogic gameLogic)
        {
            gameLogic.GetType().GetMethods().ToList()
                .ForEach(m =>
                {
                    GameLogicEvent gameLogicEvent = Attribute.GetCustomAttribute(m, typeof(GameLogicEvent)) as GameLogicEvent;
                    if (gameLogicEvent != null)
                    {
                        Type gameLogicType = gameLogic.GetType();
                        if (!_gameLogicEvents.ContainsKey(gameLogicType))
                        {
                            _gameLogicEvents.Add(gameLogicType, new Dictionary<GameLogicEventConstants.GameLogicEvent, Dictionary<GameLogic, List<MethodInfo>>>());
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
