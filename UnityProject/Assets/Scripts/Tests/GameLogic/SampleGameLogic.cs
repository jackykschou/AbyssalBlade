#if DEBUG
using ComponentEvent = Assets.Scripts.Constants.ComponentEvent;
using Assets.Scripts.Tests.SerializableComponent;
using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

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

            TriggerGameLogicEvent(this, GameLogicEvent.Example);
            TriggerGameLogicEvent<SampleGameLogic>(GameLogicEvent.Example);
            TriggerGameLogicEvent(GameLogicEvent.Example);

            TriggerGameEvent(GameEvent.ExampleEvent);
            TriggerGameEvent(this, GameEvent.ExampleEvent);
        }

        protected override void Deinitialize()
        {
            Debug.Log("SampleGameLogic Deinitialize");
        }

        [GameLogicEventAttribute(GameLogicEvent.Example)]
        public void GameLogicEventPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventPublic");
        }

        [GameLogicEventAttribute(GameLogicEvent.Example)]
        private void GameLogicEventPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventPrivate");
        }

        [GameLogicEventAttribute(GameLogicEvent.Example)]
        public static void GameLogicEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPublic");
        }

        [GameLogicEventAttribute(GameLogicEvent.Example)]
        private static void GameLogicEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPrivate");
        }

        [GameEventtAttribute(GameEvent.ExampleEvent)]
        public void GameEventPublic()
        {
            Debug.Log("SampleGameLogic GameEventPublic");
        }

        [GameEventtAttribute(GameEvent.ExampleEvent)]
        private void GameEventPrivate()
        {
            Debug.Log("SampleGameLogic GameEventPrivate");
        }

        [GameEventtAttribute(GameEvent.ExampleEvent)]
        public static void GameEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameEventStaticPublic");
        }

        [GameEventtAttribute(GameEvent.ExampleEvent)]
        private static void GameEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameEventStaticPrivate");
        }
    }
}
#endif
