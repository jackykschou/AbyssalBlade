using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnHold : PlayerInput
    {
        protected bool IsHolding;

        public override void Initialize()
        {
            base.Initialize();
            IsHolding = false;
        }

        public override void Update()
        {
            if (IsHolding && IsKeyReleased())
            {
                IsHolding = false;
                CoolDownTimeDispatcher.Dispatch();
            }
        }

        public override bool Detect()
        {
            if ((base.Detect() || IsHolding) && IsKeyOnHold())
            {
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
