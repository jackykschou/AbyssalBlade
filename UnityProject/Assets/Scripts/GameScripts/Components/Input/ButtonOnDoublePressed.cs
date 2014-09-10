using Assets.Scripts.Constants;
using Assets.Scripts.Utility;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnDoublePressed : PlayerInput
    {
        private FixTimeDispatcher _clickBufferTimeDispatcher;

        public override void Initialize()
        {
            base.Initialize();
            _clickBufferTimeDispatcher = new FixTimeDispatcher(InputConstants.DoubleClickBufferTime);
        }

        public override bool Detect()
        {
            if (base.Detect() && IsKeyDoubleClicked())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
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
            return UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(KeyCode));
        }

        private bool HasClickedOnce()
        {
            return !_clickBufferTimeDispatcher.Dispatch();
        }
    }
}
