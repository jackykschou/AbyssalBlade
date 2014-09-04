#if DEBUG
using Assets.Scripts.Attrubutes;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    [System.Serializable]
    public class SampleComponent : GameScripts.Components.SerializableComponent
    {
        public override void Initialize()
        {
            Debug.Log("SampleComponent Initialize");

            TriggerComponentEvent<SampleComponent>(ComponentEventConstants.ComponentEvent.Example);
            TriggerComponentEvent(ComponentEventConstants.ComponentEvent.Example);

            TriggerGameEvent(GameEventConstants.GameEvent.ExampleEvent);
        }

        public override void Deinitialize()
        {
            Debug.Log("SampleComponent Deinitialize");
        }

        [ComponentEvent(ComponentEventConstants.ComponentEvent.Example)]
        public void PublicEvent()
        {
            Debug.Log("SampleComponent PublicEvent");
        }

        [ComponentEvent(ComponentEventConstants.ComponentEvent.Example)]
        private void PrivateEvent()
        {
            Debug.Log("SampleComponent PrivateEvent");
        }

        [ComponentEvent(ComponentEventConstants.ComponentEvent.Example)]
        public static void StaticPublicEvent()
        {
            Debug.Log("SampleComponent StaticPublicEvent");
        }

        [ComponentEvent(ComponentEventConstants.ComponentEvent.Example)]
        private static void StaticPrivateEvent()
        {
            Debug.Log("SampleComponent StaticPrivateEvent");
        }
    }
}
#endif
