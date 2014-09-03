using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts
{
    public abstract class GameScript : MonoBehaviour
    {
        private Dictionary<Type, Dictionary<ComponentEventConstants.ComponentEvent, Dictionary<SerializableComponent, List<MethodInfo>>>> _componentsEvents;

        protected abstract void Initialize();

        protected abstract void Deinitialize();

        public void TriggerComponentEvent(ComponentEventConstants.ComponentEvent componentEvent, params object[] args)
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

        public void TriggerComponentEvent<T>(ComponentEventConstants.ComponentEvent componentEvent, params object[] args) where T : SerializableComponent
        {
            if (ContainsComponentEvent<T>(componentEvent))
            {
                foreach (var pair in _componentsEvents[typeof(T)][componentEvent])
                {
                    pair.Value.ForEach(m => m.Invoke(pair.Key, args));
                }
            }
        }

        private bool ContainsComponentEvent<T>(ComponentEventConstants.ComponentEvent componentEvent) where T : SerializableComponent
        {
            return _componentsEvents.ContainsKey(typeof(T)) && _componentsEvents[typeof(T)].ContainsKey(componentEvent);
        }

        public void TriggerComponentEvent(SerializableComponent component, ComponentEventConstants.ComponentEvent componentEvent, params object[] args)
        {
            if (ContainsComponentEvent(component, componentEvent))
            {
                foreach (var m in _componentsEvents[component.GetType()][componentEvent][component])
                {
                   m.Invoke(component, args);
                }
            }
        }

        private bool ContainsComponentEvent(SerializableComponent component, ComponentEventConstants.ComponentEvent componentEvent)
        {
            return _componentsEvents.ContainsKey(component.GetType()) && _componentsEvents[component.GetType()].ContainsKey(componentEvent) && _componentsEvents[component.GetType()][componentEvent].ContainsKey(component);
        }

        public void TriggerGameEvent(GameEventConstants.GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
        }

        void Awake ()
        {
            InitializeFields();
        }

        void Start()
        {
            SubscribeGameEvents();
            InitializeComponents();
            Initialize();
        }

        void OnSpawned()
        {
            
        }

        void OnDestroy()
        {
            DeinitializeComponents();
            UnsubscribeGameEvents();
            Deinitialize();
        }

        void OnDespawned()
        {
            
        }

        private void InitializeFields()
        {
            _componentsEvents = new Dictionary<Type, Dictionary<ComponentEventConstants.ComponentEvent, Dictionary<SerializableComponent, List<MethodInfo>>>>();
        }
	
        private void InitializeComponents()
        {
            var components =
                GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Select(f => f.GetValue(this) as SerializableComponent)
                            .Where(c => c != null).ToList();

            foreach (var component in components)
            {
                component.GameScript = this;
                AddComponentEvents(component);
                component.SubscribeGameEvents();
            }

            foreach (var component in components)
            {
                component.Initialize();
            }
        }

        private void DeinitializeComponents()
        {
            var components =
                GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Select(f => f.GetValue(this) as SerializableComponent)
                            .Where(c => c != null).ToList();

            foreach (var component in components)
            {
                component.Deinitialize();
            }

            foreach (var component in components)
            {
                component.UnsubscribeGameEvents();
            }
        }

        private void AddComponentEvents(SerializableComponent component)
        {
            component.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    ComponentEvent componentEvent = Attribute.GetCustomAttribute(m, typeof(ComponentEvent)) as ComponentEvent;
                    if (componentEvent != null)
                    {
                        Type componentType = component.GetType();
                        if (!_componentsEvents.ContainsKey(componentType))
                        {
                            _componentsEvents.Add(componentType, new Dictionary<ComponentEventConstants.ComponentEvent, Dictionary<SerializableComponent, List<MethodInfo>>>());
                        }
                        if (!_componentsEvents[componentType].ContainsKey(componentEvent.Event))
                        {
                            _componentsEvents[componentType].Add(componentEvent.Event, new Dictionary<SerializableComponent, List<MethodInfo>>());
                        }
                        if (!_componentsEvents[componentType][componentEvent.Event].ContainsKey(component))
                        {
                            _componentsEvents[componentType][componentEvent.Event].Add(component, new List<MethodInfo>());
                        }
                        _componentsEvents[componentType][componentEvent.Event][component].Add(m);
                    }
                });
        }

        private void SubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEvent))
                    {
                        GameEvent gameEventSubscriberAttribute = attr as GameEvent;
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
                    if (attr.GetType() == typeof(GameEvent))
                    {
                        GameEvent gameEventSubscriberAttribute = attr as GameEvent;
                        GameEventManager.Instance.UnsubscribeGameEvent(this, gameEventSubscriberAttribute.Event);
                    }
                }
            }
        }
    }
}
