using Assets.Scripts.Attributes;
using ComponentEvent = Assets.Scripts.Constants.ComponentEvent;
#if DEBUG
using Assets.Scripts.Tests.SerializableComponent;
using UnityEngine;

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

            TriggerGameLogicEvent(this, Constants.GameLogicEvent.Example);
            TriggerGameLogicEvent<SampleGameLogic>(Constants.GameLogicEvent.Example);
            TriggerGameLogicEvent(Constants.GameLogicEvent.Example);

            TriggerGameEvent(Constants.GameEvent.ExampleEvent);
        }

        protected override void Deinitialize()
        {
            Debug.Log("SampleGameLogic Deinitialize");
        }

        [GameLogicEvent(Constants.GameLogicEvent.Example)]
        public void GameLogicEventPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventPublic");
        }

        [GameLogicEvent(Constants.GameLogicEvent.Example)]
        private void GameLogicEventPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventPrivate");
        }

        [GameLogicEvent(Constants.GameLogicEvent.Example)]
        public static void GameLogicEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPublic");
        }

        [GameLogicEvent(Constants.GameLogicEvent.Example)]
        private static void GameLogicEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameLogicEventStaticPrivate");
        }

        [GameEvent(Constants.GameEvent.ExampleEvent)]
        public void GameEventPublic()
        {
            Debug.Log("SampleGameLogic GameEventPublic");
        }

        [GameEvent(Constants.GameEvent.ExampleEvent)]
        private void GameEventPrivate()
        {
            Debug.Log("SampleGameLogic GameEventPrivate");
        }

        [GameEvent(Constants.GameEvent.ExampleEvent)]
        public static void GameEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameEventStaticPublic");
        }

        [GameEvent(Constants.GameEvent.ExampleEvent)]
        private static void GameEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameEventStaticPrivate");
        }
    }
}
#endif
