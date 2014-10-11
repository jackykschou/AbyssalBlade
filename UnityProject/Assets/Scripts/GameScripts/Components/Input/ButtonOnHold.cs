using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnHold : PlayerInput
    {
        protected bool IsHolding;

        public float LastHoldTime {
            get { return _lastReleaseTime - _lastHoldStartTime; }
        }
        private float _lastHoldStartTime;
        private float _lastReleaseTime;

        public override void Initialize()
        {
            base.Initialize();
            IsHolding = false;
            _lastHoldStartTime = 0f;
            _lastReleaseTime = 0f;
        }

        public override void Update()
        {
            base.Update();
            if (IsHolding && IsKeyReleased())
            {
                IsHolding = false;
                CoolDownTimeDispatcher.Dispatch();
                _lastReleaseTime = Time.fixedTime;
            }
        }

        public override bool Detect()
        {
            if ((base.Detect() || IsHolding) && IsKeyOnHold())
            {
                if (!IsHolding)
                {
                    _lastHoldStartTime = Time.fixedTime;
                }
                IsHolding = true;
                return true;
            }
            return false;
        }

        private bool IsKeyOnHold()
        {
            return UnityEngine.Input.GetButton(InputConstants.GetKeyCodeName(KeyCode));
        }

        private bool IsKeyReleased()
        {
            return UnityEngine.Input.GetButtonUp(InputConstants.GetKeyCodeName(KeyCode));
        }
    }
}
