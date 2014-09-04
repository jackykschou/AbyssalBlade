using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GameScripts.Components
{
    public abstract class SerializableComponent
    {
        public GameScript GameScript { protected get; set; }

        public abstract void Initialize();

        public abstract void Deinitialize();

        public void TriggerGameEvent(GameEventConstants.GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
        }

        public void TriggerComponentEvent(ComponentEventConstants.ComponentEvent componentEvent, params object[] args)
        {
            GameScript.TriggerComponentEvent(componentEvent, args);
        }

        public void TriggerComponentEvent<T>(ComponentEventConstants.ComponentEvent componentEvent, params object[] args) where T : SerializableComponent
        {
            GameScript.TriggerComponentEvent<T>(componentEvent, args);
        }

        public void SubscribeGameEvents()
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

        public void UnsubscribeGameEvents()
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

        protected List<T> GetComponents<T>() where T : SerializableComponent
        {
            List<T> components = ((GameScript.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Select(f => f.GetValue(GameScript))
                .Where(v => (v is T)).ToList())) as List<T>;

            if (components != null && components.Count == 0)
            {
                return null;
            }
            return components;
        }

        protected T GetComponent<T>() where T : SerializableComponent
        {
            return (GameScript.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Select(f => f.GetValue(GameScript))
                .FirstOrDefault(v => (v is T))) as T ;
        }
    }
}
