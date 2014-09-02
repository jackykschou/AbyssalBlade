using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnReleased : SerializableComponent, IInput
    {
        [SerializeField]
        private float _coolDown;
        [SerializeField]
        private InputConstants.InputKeyCode _keyCode;

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
            if (!IsInCooldown() && IsKeyReleased())
            {
                _timeDispatcher.Dispatch();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsInCooldown()
        {
            return !_timeDispatcher.CanDispatch();
        }

        private bool IsKeyReleased()
        {
            return UnityEngine.Input.GetButtonUp(InputConstants.GetKeyCodeName(_keyCode));
        }
    }
}
