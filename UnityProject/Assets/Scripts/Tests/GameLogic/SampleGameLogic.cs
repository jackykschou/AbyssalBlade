using UnityEngine.EventSystems;
#if DEBUG
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class SampleGameLogic : GameLogic
    {
        public SampleComponent Component;

        public SampleComponent Component2;

        protected override void Initialize()
        {
            Debug.Log("SampleGameLogic Initialize");

            TriggerComponentEvent<SampleComponent>(ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(Component, ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(Component2, ComponentEventConstants.ComponentEvent.Example);

            TriggerGameLogicEvent(this, GameLogicEventConstants.GameLogicEvent.Example);
            TriggerGameLogicEvent<SampleGameLogic>(GameLogicEventConstants.GameLogicEvent.Example);

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
