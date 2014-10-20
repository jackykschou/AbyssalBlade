using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnDoublePressed : PlayerInput
    {
        public FixTimeDispatcher ClickBufferTimeDispatcher;

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
            return !ClickBufferTimeDispatcher.Dispatch();
        }
    }
}
