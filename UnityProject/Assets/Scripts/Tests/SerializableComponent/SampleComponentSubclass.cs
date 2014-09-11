#if DEBUG
using UnityEngine;

namespace Assets.Scripts.Tests.SerializableComponent
{
    [System.Serializable]
    public class SampleComponentSubclass : SampleComponent 
    {
        public override void Initialize()
        {
            Debug.Log("SampleComponentSubclass Initialize");
        }
    }
}
#endif
