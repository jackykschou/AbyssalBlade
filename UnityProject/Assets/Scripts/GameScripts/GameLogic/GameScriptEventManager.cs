using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameScriptEventManager : MonoBehaviour 
    {
        private Dictionary<Type, Dictionary<Constants.GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>> _gameScriptEvents;

        void Awake()
        {
            InitializeFields();
            InitializeGameScriptEvents();
        }

        public void TriggerGameScriptEvent(Constants.GameScriptEvent gameScriptEvent, params object[] args)
        {
            foreach (var value in _gameScriptEvents.Values)
            {
                if (value.ContainsKey(gameScriptEvent))
                {
                    foreach (var pair in value[gameScriptEvent])
                    {
                        pair.Value.ForEach(m => m.Invoke(pair.Key, args));
                    }
                }
            }
        }

        public void TriggerGameScriptEvent<T>(Constants.GameScriptEvent gameScriptEvent, params object[] args) where T : GameScript
        {
            foreach (var typeDictPair in _gameScriptEvents)
            {
                if ((typeof(T) == (typeDictPair.Key) || typeDictPair.Key.IsSubclassOf(typeof(T))) && typeDictPair.Value.ContainsKey(gameScriptEvent))
                {
                    foreach (var gameScriptMethodsPair in typeDictPair.Value[gameScriptEvent])
                    {
                        gameScriptMethodsPair.Value.ForEach(m => m.Invoke(gameScriptMethodsPair.Key, args));
                    }
                }
            }
        }

        public void TriggerGameScriptEvent(GameScript gameScript, Constants.GameScriptEvent gameScriptEvent, params object[] args)
        {
            if (ContainGameScriptEvent(gameScript, gameScriptEvent))
            {
                foreach (var m in _gameScriptEvents[gameScript.GetType()][gameScriptEvent][gameScript])
                {
                    m.Invoke(gameScript, args);
                }
            }
        }

        private bool ContainGameScriptEvent(GameScript gameScript, Constants.GameScriptEvent gameScriptEvent)
        {
            return _gameScriptEvents.ContainsKey(gameScript.GetType()) && _gameScriptEvents[gameScript.GetType()].ContainsKey(gameScriptEvent) && _gameScriptEvents[gameScript.GetType()][gameScriptEvent].ContainsKey(gameScript);
        }

        private void InitializeFields()
        {
            _gameScriptEvents = new Dictionary<Type, Dictionary<Constants.GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>>();
        }

        private void InitializeGameScriptEvents()
        {
            foreach (var gameScript in GetComponents<GameScript>())
            {
                gameScript.GameScriptEventManager = this;
                AddGameScriptEvents(gameScript);
            }
        }

        private void AddGameScriptEvents(GameScript gameScript)
        {
            gameScript.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    GameScriptEvent gameScriptEvent = Attribute.GetCustomAttribute(m, typeof(GameScriptEvent)) as GameScriptEvent;
                    if (gameScriptEvent != null)
                    {
                        Type gameScriptType = gameScript.GetType();
                        if (!_gameScriptEvents.ContainsKey(gameScriptType))
                        {
                            _gameScriptEvents.Add(gameScriptType, new Dictionary<Constants.GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>());
                        }
                        if (!_gameScriptEvents[gameScriptType].ContainsKey(gameScriptEvent.Event))
                        {
                            _gameScriptEvents[gameScriptType].Add(gameScriptEvent.Event, new Dictionary<GameScript, List<MethodInfo>>());
                        }
                        if (!_gameScriptEvents[gameScriptType][gameScriptEvent.Event].ContainsKey(gameScript))
                        {
                            _gameScriptEvents[gameScriptType][gameScriptEvent.Event].Add(gameScript, new List<MethodInfo>());
                        }
                        _gameScriptEvents[gameScriptType][gameScriptEvent.Event][gameScript].Add(m);
                    }
                });
        }
    }
}
