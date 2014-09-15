﻿#if DEBUG
using Assets.Scripts.Attributes;
using ComponentEvent = Assets.Scripts.Constants.ComponentEvent;
using Assets.Scripts.Tests.SerializableComponent;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Tests.GameLogic
{
    public class SampleGameLogic : GameScripts.GameLogic.GameLogic
    {
        public SampleComponent Component;

        public SampleComponentSubclass SubclassComponent;

        protected override void Initialize()
        {
            Debug.Log("SampleGameLogic Initialize");

            TriggerComponentEvent<SampleComponent>(ComponentEvent.Example);
            TriggerComponentEvent<SampleComponentSubclass>(ComponentEvent.Example);
            TriggerComponentEvent(ComponentEvent.Example);
            TriggerComponentEvent(Component, ComponentEvent.Example);
            TriggerComponentEvent(SubclassComponent, ComponentEvent.Example);

            TriggerGameScriptEvent(this, Constants.GameScriptEvent.Example);
            TriggerGameScriptEvent<SampleGameLogic>(Constants.GameScriptEvent.Example);
            TriggerGameScriptEvent(Constants.GameScriptEvent.Example);

            TriggerGameEvent(GameEvent.ExampleEvent);
            TriggerGameEvent(this, GameEvent.ExampleEvent);
        }

        protected override void Deinitialize()
        {
            Debug.Log("SampleGameLogic Deinitialize");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        public void GameScriptEventPublic()
        {
            Debug.Log("SampleGameLogic GameScriptEventPublic");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        private void GameScriptEventPrivate()
        {
            Debug.Log("SampleGameLogic GameScriptEventPrivate");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        public static void GameScriptEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameScriptEventStaticPublic");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        private static void GameScriptEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameScriptEventStaticPrivate");
        }

        [GameEventAttribute(GameEvent.ExampleEvent)]
        public void GameEventPublic()
        {
            Debug.Log("SampleGameLogic GameEventPublic");
        }

        [GameEventAttribute(GameEvent.ExampleEvent)]
        private void GameEventPrivate()
        {
            Debug.Log("SampleGameLogic GameEventPrivate");
        }

        [GameEventAttribute(GameEvent.ExampleEvent)]
        public static void GameEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameEventStaticPublic");
        }

        [GameEventAttribute(GameEvent.ExampleEvent)]
        private static void GameEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameEventStaticPrivate");
        }
    }
}
#endif
