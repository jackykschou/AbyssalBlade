using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnPressed : SerializableComponent, IInput
    {
        [SerializeField]
        private float _coolDown;
        [SerializeField]
        private InputKeyCode _keyCode;

        private FixTimeDispatcher _timeDispatcher;

        public override void Initialize()
        {
            _timeDispatcher = new FixTimeDispatcher(_coolDown);
        }

        public override void Deinitialize()
        {
        }

        public bool Detect()
        {
            if (!IsInCooldown() && IsKeyPressed())
            {
                _timeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsInCooldown()
        {
            return !_timeDispatcher.CanDispatch();
        }

        private bool IsKeyPressed()
        {
            return UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(_keyCode));
        }
    }
}
