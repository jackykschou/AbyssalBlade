using System;
using UnityEngine;

//todo remove
using Assets.Scripts.Managers;

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
            //Target = transform.parent;
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
            handleMusicTest();
            if (Target == null)
            {
                return;;
            }

            Vector3 wantedPosition = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Damping);
            this.gameObject.transform.position = new Vector3(wantedPosition.x, wantedPosition.y, transform.position.z);
        }
        void handleMusicTest()
        {
            // sound system checking
            
            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad0))
                AudioManager.Instance.playCue("Parallel1");
            
            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad1))
                AudioManager.Instance.playCue("Random1");
            /*
            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad2))

            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad3))

            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad4))

            // WORKING 
            if (Input.GetKeyDown(KeyCode.Keypad5))

            // WORKING 
            if (Input.GetKeyDown(KeyCode.Keypad6))

            // WORKING 
            if (Input.GetKeyDown(KeyCode.Keypad7))

            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad8))

            // WORKING
            if (Input.GetKeyDown(KeyCode.Keypad9))
                */


        }
    }
    

}
