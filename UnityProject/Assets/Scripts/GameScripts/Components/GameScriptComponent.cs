using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Managers;
using ComponentEvent = Assets.Scripts.Constants.ComponentEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.Components
{
    public abstract class GameScriptComponent
    {
        public GameScript GameScript { protected get; set; }

        private List<GameScriptComponent> _components;

        public void InitializeChildComponents()
        {
            _components = new List<GameScriptComponent>();
            _components =
                GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance |
                               BindingFlags.Static)
                    .Select(f => f.GetValue(this) as GameScriptComponent)
                    .Where(c => c != null).ToList();

            _components.ForEach(c =>
            {
                c.Initialize();
                c.InitializeChildComponents();
            });
        }

        public void DeinitializeChildComponents()
        {
            _components.ForEach(c =>
            {
                c.DeinitializeChildComponents();
                c.Deinitialize();
            });
        }

        public abstract void Initialize();

        public abstract void Deinitialize();

        public abstract void Update();

        public virtual void EditorUpdate()
        {
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
        }

        public void TriggerComponentEvent(ComponentEvent componentEvent, params object[] args)
        {
            GameScript.TriggerComponentEvent(componentEvent, args);
        }

        public void TriggerComponentEvent<T>(ComponentEvent componentEvent, params object[] args) where T : GameScriptComponent
        {
            GameScript.TriggerComponentEvent<T>(componentEvent, args);
        }

        public void SubscribeGameEvents()
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

        public void UnsubscribeGameEvents()
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

        protected List<T> GetGameScriptComponents<T>() where T : GameScriptComponent
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

        protected T GetGameScriptComponent<T>() where T : GameScriptComponent
        {
            return (GameScript.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Select(f => f.GetValue(GameScript))
                .FirstOrDefault(v => (v is T))) as T ;
        }
    }
}
