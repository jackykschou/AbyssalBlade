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

        public void TriggerComponentEvent<T>(ComponentEventConstants.ComponentEvent componentEvent, params object[] args) where T : SerializableComponent
        {
            if (ContainsComponentEvent<T>(componentEvent))
            {
                foreach (KeyValuePair<SerializableComponent, List<MethodInfo>> pair in _componentsEvents[typeof(T)][componentEvent])
                {
                    SerializableComponent component = pair.Key;
                    pair.Value.ForEach(m => m.Invoke(component, args));
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
                foreach (MethodInfo m in _componentsEvents[component.GetType()][componentEvent][component])
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
            GameEventManager.TriggerGameEvent(gameEvent, args);
        }

        void Awake ()
        {
            InitializeFields();
            InitializeComponents();
            SubscribeGameEvents();
            Initialize();
        }

        void OnDestroy()
        {
            DeinitializeComponents();
            UnsubscribeGameEvents();
            Deinitialize();
        }

        private void InitializeFields()
        {
            _componentsEvents = new Dictionary<Type, Dictionary<ComponentEventConstants.ComponentEvent, Dictionary<SerializableComponent, List<MethodInfo>>>>();
        }
	
        private void InitializeComponents()
        {
            foreach (FieldInfo info in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                SerializableComponent component = info.GetValue(this) as SerializableComponent;
                if (component != null && info.GetValue(this) is SerializableComponent)
                {
                    component.GameScript = this; 
                    component.Initialize();
                    component.SubscribeGameEvents();
                    AddComponentEvents(component);
                }
            }
        }

        private void DeinitializeComponents()
        {
            foreach (FieldInfo info in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                SerializableComponent component = info.GetValue(this) as SerializableComponent;
                if (component != null && info.GetValue(this) is SerializableComponent)
                {
                    component.Deinitialize();
                    component.UnsubscribeGameEvents();
                }
            }
        }

        private void AddComponentEvents(SerializableComponent component)
        {
            component.GetType().GetMethods().ToList()
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
            foreach (MethodInfo info in this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEvent))
                    {
                        GameEvent gameEventSubscriberAttribute = attr as GameEvent;
                        Delegate eventDelegate = Delegate.CreateDelegate(GameEventConstants.GetEventType(gameEventSubscriberAttribute.Event), info);
                        GameEventManager.SubscribeGameEvent(this, gameEventSubscriberAttribute.Event, eventDelegate);
                    }
                }
            }
        }

        private void UnsubscribeGameEvents()
        {
            foreach (MethodInfo info in this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEvent))
                    {
                        GameEvent gameEventSubscriberAttribute = attr as GameEvent;
                        Delegate eventDelegate = Delegate.CreateDelegate(GameEventConstants.GetEventType(gameEventSubscriberAttribute.Event), info);
                        GameEventManager.UnsubscribeGameEvent(this, gameEventSubscriberAttribute.Event, eventDelegate);
                    }
                }
            }
        }
    }
}
