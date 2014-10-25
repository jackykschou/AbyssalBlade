using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameScriptEventManager : MonoBehaviour 
    {
        private Dictionary<Type, Dictionary<Constants.GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>> _gameScriptEvents;
        private List<GameScript> _gameScripts;
        public bool Initialized 
        {
            get
            {
                return _gameScripts.All(s => s.Initialized);
                Debug.Log("_gameScriptsInitialized: " + _gameScriptsInitialized + " _gameScripts.All(s => s.Initialized): " + (_gameScripts.All(s => s.Initialized)));
                return _initialized && _gameScriptsInitialized;
            }
        }

        public bool Disabled
        {
            get { return _gameScripts.Any(s => s.Disabled); }
        }

        private bool _gameScriptsInitialized = false;
        private bool _firstTimeInitialized = false;
        private bool _initialized = false;
        private bool _deinitialized = false;

        public void UpdateInitialized()
        {
            _gameScriptsInitialized = _gameScripts.All(s => s.Initialized);
        }

        public void TriggerGameScriptEvent(Constants.GameScriptEvent gameScriptEvent, params object[] args)
        {
            foreach (var value in _gameScriptEvents.Values)
            {
                if (value.ContainsKey(gameScriptEvent))
                {
                    foreach (var pair in value[gameScriptEvent])
                    {
                        if (pair.Key.Initialized)
                        {
                            pair.Value.ForEach(m => m.Invoke(pair.Key, args));
                        }
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
                        if (gameScriptMethodsPair.Key.Initialized)
                        {
                            gameScriptMethodsPair.Value.ForEach(m => m.Invoke(gameScriptMethodsPair.Key, args));
                        }
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
                    if (gameScript.Initialized)
                    {
                        m.Invoke(gameScript, args);
                    }
                }
            }
        }

        private bool ContainGameScriptEvent(GameScript gameScript, Constants.GameScriptEvent gameScriptEvent)
        {
            return _gameScriptEvents.ContainsKey(gameScript.GetType()) && _gameScriptEvents[gameScript.GetType()].ContainsKey(gameScriptEvent) && _gameScriptEvents[gameScript.GetType()][gameScriptEvent].ContainsKey(gameScript);
        }

        void Start()
        {
            InitializeHelper();
        }

        void OnEnable()
        {
            InitializeHelper();
        }

        void OnSpawned()
        {
            InitializeHelper();
        }

        private void InitializeHelper()
        {
            if (_initialized)
            {
                return;
            }

            if (!_firstTimeInitialized)
            {
                _gameScriptEvents = new Dictionary<Type, Dictionary<Constants.GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>>();
                _gameScripts = GetComponents<GameScript>().ToList();
                _firstTimeInitialized = true;
            }
            _initialized = true;
            _deinitialized = false;
        }

        void OnDespawned()
        {
            DeinitializeHelper();
        }

        void OnDisable()
        {
            DeinitializeHelper();
        }

        void OnDestroy()
        {
            DeinitializeHelper();
        }

        private void DeinitializeHelper()
        {
            if (_deinitialized || !_initialized)
            {
                return;
            }

            _gameScriptsInitialized = false;
            _initialized = false;
            _deinitialized = true;
        }

        public void UpdateGameScriptEvents(GameScript gameScript)
        {
            InitializeHelper();
            AddGameScriptEvents(gameScript);
        }

        private void AddGameScriptEvents(GameScript gameScript)
        {
            gameScript.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    foreach (var a in Attribute.GetCustomAttributes(m, typeof(GameScriptEvent)))
                    {
                        GameScriptEvent gameScriptEvent = a as GameScriptEvent;
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
                    }
                });
        }
    }
}
