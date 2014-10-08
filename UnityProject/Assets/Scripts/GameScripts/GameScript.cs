using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

using ComponentEvent = Assets.Scripts.Constants.ComponentEvent;
using ComponentEventAttribute = Assets.Scripts.Attributes.ComponentEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts
{
    [RequireComponent(typeof(GameScriptEditorUpdate))]
    [RequireComponent(typeof(GameScriptEventManager))]
    public abstract class GameScript : MonoBehaviour
    {
        public GameScriptEventManager GameScriptEventManager { get; private set; }

        private Dictionary<Type, Dictionary<ComponentEvent, Dictionary<GameScriptComponent, List<MethodInfo>>>> _componentsEvents;
        private List<GameScriptComponent> _components;
        private bool _initialized = false;
        private bool _deinitialized = false;

        protected abstract void Initialize();

        protected abstract void Deinitialize();

        public virtual void EditorUpdate()
        {
        }

        public void TriggerGameScriptEvent(GameScriptEvent gameScriptEvent, params object[] args)
        {
            GameScriptEventManager.TriggerGameScriptEvent(gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                }
            }
        }

        public void TriggerGameScriptEvent<T>(GameScriptEvent gameScriptEvent, params object[] args) where T : GameScript
        {
            GameScriptEventManager.TriggerGameScriptEvent<T>(gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    if (s is T)
                    {
                        s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                    }
                }
            }
        }

        public void TriggerGameScriptEvent(GameScript gameScript, GameScriptEvent gameScriptEvent, params object[] args)
        {
            GameScriptEventManager.TriggerGameScriptEvent(gameScript, gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                }
            }
        }

        public void TriggerComponentEvent(ComponentEvent componentEvent, params object[] args)
        {
            foreach (var value in _componentsEvents.Values)
            {
                foreach (var pair in value)
                {
                    if (pair.Key == componentEvent)
                    {
                        foreach (var componentPair in pair.Value)
                        {
                            componentPair.Value.ForEach(m => m.Invoke(componentPair.Key, args));
                        }
                    }
                }
            }
        }

        public void TriggerComponentEvent<T>(ComponentEvent componentEvent, params object[] args) where T : GameScriptComponent
        {
            foreach (var typeDictPair in _componentsEvents)
            {
                if ((typeof(T) == (typeDictPair.Key) || typeDictPair.Key.IsSubclassOf(typeof(T))) && typeDictPair.Value.ContainsKey(componentEvent))
                {
                    foreach (var componentMethodsPair in typeDictPair.Value[componentEvent])
                    {
                        componentMethodsPair.Value.ForEach(m => m.Invoke(componentMethodsPair.Key, args));
                    }
                }
            }
        }

        public void TriggerComponentEvent(GameScriptComponent component, ComponentEvent componentEvent, params object[] args)
        {
            if (ContainsComponentEvent(component, componentEvent))
            {
                foreach (var m in _componentsEvents[component.GetType()][componentEvent][component])
                {
                   m.Invoke(component, args);
                }
            }
        }

        private bool ContainsComponentEvent(GameScriptComponent component, ComponentEvent componentEvent)
        {
            return _componentsEvents.ContainsKey(component.GetType()) && _componentsEvents[component.GetType()].ContainsKey(componentEvent) && _componentsEvents[component.GetType()][componentEvent].ContainsKey(component);
        }

        public void TriggerGameEvent(System.Object obj, GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(obj, gameEvent, args);
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
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

        void InitializeHelper()
        {
            if (_initialized)
            {
                return;
            }

            InitializeFields();
            SubscribeGameEvents();
            InitializeComponents();
            Initialize();
            gameObject.CacheGameObject();
            _deinitialized = false;
            _initialized = true;
        }

        public void DisableGameObject(float delay = 0f)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            StartCoroutine(DisableGameObjectIE(delay));
        }

        IEnumerator DisableGameObjectIE(float delay)
        {
            yield return new WaitForSeconds(delay);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectDestroyed);
            if (PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                PrefabManager.Instance.DespawnPrefab(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ImmediateDisableGameObject()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            PrefabManager.Instance.DespawnPrefab(gameObject);
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

        void DeinitializeHelper()
        {
            if (_deinitialized || !_initialized)
            {
                return;
            }

            DeinitializeComponents();
            UnsubscribeGameEvents();
            Deinitialize();
            gameObject.UncacheGameObject();
            _initialized = false;
            _deinitialized = true;
        }

        protected virtual void Update()
        {
            _components.ForEach(c => c.Update());
        }

        protected virtual void FixedUpdate()
        {
            _components.ForEach(c => c.FixedUpdate());
        }

        private void InitializeFields()
        {
            _componentsEvents = new Dictionary<Type, Dictionary<ComponentEvent, Dictionary<GameScriptComponent, List<MethodInfo>>>>();
            _components = new List<GameScriptComponent>();
            GameScriptEventManager = GetComponent<GameScriptEventManager>();
            GameScriptEventManager.UpdateGameScriptEvents(this);
        }
	
        private void InitializeComponents()
        {
            _components =
                GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Select(f => f.GetValue(this) as GameScriptComponent)
                            .Where(c => c != null).ToList();

            foreach (var component in _components)
            {
                component.GameScript = this;
                AddComponentEvents(component);
                component.SubscribeGameEvents();
            }

            foreach (var component in _components)
            {
                component.Initialize();
                component.InitializeChildComponents();
            }
        }

        private void DeinitializeComponents()
        {
            foreach (var component in _components)
            {
                component.DeinitializeChildComponents();
                component.Deinitialize();
            }

            foreach (var component in _components)
            {
                component.UnsubscribeGameEvents();
            }
        }

        private void AddComponentEvents(GameScriptComponent component)
        {
            component.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    foreach (var a in Attribute.GetCustomAttributes(m, typeof(ComponentEventAttribute)))
                    {
                        ComponentEventAttribute componentEvent = a as ComponentEventAttribute;
                        if (componentEvent != null)
                        {
                            Type componentType = component.GetType();
                            if (!_componentsEvents.ContainsKey(componentType))
                            {
                                _componentsEvents.Add(componentType, new Dictionary<ComponentEvent, Dictionary<GameScriptComponent, List<MethodInfo>>>());
                            }
                            if (!_componentsEvents[componentType].ContainsKey(componentEvent.Event))
                            {
                                _componentsEvents[componentType].Add(componentEvent.Event, new Dictionary<GameScriptComponent, List<MethodInfo>>());
                            }
                            if (!_componentsEvents[componentType][componentEvent.Event].ContainsKey(component))
                            {
                                _componentsEvents[componentType][componentEvent.Event].Add(component, new List<MethodInfo>());
                            }
                            _componentsEvents[componentType][componentEvent.Event][component].Add(m);
                        }
                    }
                });
        }

        private void SubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.SubscribeGameEvent(this, gameEventSubscriberAttribute.Event, info);
                    }
                }
            }
        }

        private void UnsubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.UnsubscribeGameEvent(this, gameEventSubscriberAttribute.Event);
                    }
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        { }

        protected virtual void OnTriggerStay2D(Collider2D coll)
        { }
    }
}
