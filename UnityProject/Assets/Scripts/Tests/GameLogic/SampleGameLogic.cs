using Assets.Scripts.Tests.SerializableComponent;
#if DEBUG
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class SampleGameLogic : GameScripts.GameLogic.GameLogic
    {
        public SampleComponent Component;

        public SampleComponentSubclass SubclassComponent;

        protected override void Initialize()
        {
            Debug.Log("SampleGameLogic Initialize");

            TriggerComponentEvent<SampleComponent>(ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent<SampleComponentSubclass>(ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(Component, ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(SubclassComponent, ComponentEventConstants.ComponentEvent.Example);

            TriggerGameLogicEvent(this, GameLogicEventConstants.GameLogicEvent.Example);
            TriggerGameLogicEvent<SampleGameLogic>(GameLogicEventConstants.GameLogicEvent.Example);
            TriggerGameLogicEvent(GameLogicEventConstants.GameLogicEvent.Example);

            TriggerGameEvent(GameEventConstants.GameEvent.ExampleEvent);
        }

        protected override void Deinitialize()
        {
            Debug.Log("SampleGameLogic Deinitialize");
        }

        [GameLogicEvent(GameLogicEventConstants.GameLogicEvent.Example)]
        public void GameLogicEventPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventPublic");
        }

        [GameLogicEvent(GameLogicEventConstants.GameLogicEvent.Example)]
        private void GameLogicEventPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventPrivate");
        }

        [GameLogicEvent(GameLogicEventConstants.GameLogicEvent.Example)]
        public static void GameLogicEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPublic");
        }

        [GameLogicEvent(GameLogicEventConstants.GameLogicEvent.Example)]
        private static void GameLogicEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPrivate");
        }

        [GameEvent(GameEventConstants.GameEvent.ExampleEvent)]
        public void GameEventPublic()
        {
            Debug.Log("SampleGameLogic GameEventPublic");
        }

        [GameEvent(GameEventConstants.GameEvent.ExampleEvent)]
        private void GameEventPrivate()
        {
            Debug.Log("SampleGameLogic GameEventPrivate");
        }

        [GameEvent(GameEventConstants.GameEvent.ExampleEvent)]
        public static void GameEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameEventStaticPublic");
        }

        [GameEvent(GameEventConstants.GameEvent.ExampleEvent)]
        private static void GameEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameEventStaticPrivate");
        }
    }
}
#endif
