using System;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraFollow : GameLogic
    {
        public Transform Target;

        [Range(0, float.MaxValue)]
        public float Damping = 5f;

        protected override void Initialize()
        {
            base.Initialize();
            Target = transform.parent;
            if (Target == null)
            {
                throw new Exception("Need a parent to follow");
            }
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();

            if (Target == null)
            {
                return;;
            }

            Vector3 wantedPosition = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Damping);
            transform.position = new Vector3(wantedPosition.x, wantedPosition.y, transform.position.z);
        }
    }
}
