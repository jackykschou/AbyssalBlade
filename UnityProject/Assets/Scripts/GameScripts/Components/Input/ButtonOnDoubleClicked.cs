using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnDoubleClicked : SerializableComponent, IInput
    {
        [SerializeField]
        private float _coolDown;
        [SerializeField]
        private InputConstants.InputKeyCode _keyCode;

        private FixTimeDispatcher _clickBufferTimeDispatcher;
        private FixTimeDispatcher _coolDownTimeDispatcher;

        public override void Initialize()
        {
            _clickBufferTimeDispatcher = new FixTimeDispatcher(InputConstants.DoubleClickBufferTime);
            _coolDownTimeDispatcher = new FixTimeDispatcher(_coolDown);
        }

        public override void Deinitialize()
        {
        }

        public bool Detect()
        {
            if (!IsInCooldown() && IsKeyDoubleClicked())
            {
                _coolDownTimeDispatcher.Dispatch();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsInCooldown()
        {
            return !_coolDownTimeDispatcher.CanDispatch();
        }

        private bool IsKeyDoubleClicked()
        {
            if (IsKeyPressed())
            {
                if (HasClickedOnce())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsKeyPressed()
        {
            return UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(_keyCode));
        }

        private bool HasClickedOnce()
        {
            return !_clickBufferTimeDispatcher.Dispatch();
        }
    }
}
