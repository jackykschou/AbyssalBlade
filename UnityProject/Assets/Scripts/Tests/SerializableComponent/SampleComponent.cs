#if DEBUG

using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Tests.SerializableComponent
{
    [System.Serializable]
    public class SampleComponent : GameScripts.Components.SerializableComponent
    {
        public override void Initialize()
        {
            Debug.Log("SampleComponent Initialize");

            TriggerComponentEvent<SampleComponent>(ComponentEvent.Example);
            TriggerComponentEvent(ComponentEvent.Example);

            TriggerGameEvent(GameEvent.ExampleEvent);
        }

        public override void Deinitialize()
        {
            Debug.Log("SampleComponent Deinitialize");
        }

        public override void Update()
        {
        }

        [Attributes.ComponentEvent(ComponentEvent.Example)]
        public void PublicEvent()
        {
            Debug.Log("SampleComponent PublicEvent");
        }

        [Attributes.ComponentEvent(ComponentEvent.Example)]
        private void PrivateEvent()
        {
            Debug.Log("SampleComponent PrivateEvent");
        }

        [Attributes.ComponentEvent(ComponentEvent.Example)]
        public static void StaticPublicEvent()
        {
            Debug.Log("SampleComponent StaticPublicEvent");
        }

        [Attributes.ComponentEvent(ComponentEvent.Example)]
        private static void StaticPrivateEvent()
        {
            Debug.Log("SampleComponent StaticPrivateEvent");
        }
    }
}
#endif
