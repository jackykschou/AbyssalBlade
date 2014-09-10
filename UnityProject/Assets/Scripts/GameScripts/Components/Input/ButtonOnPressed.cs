using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnPressed : PlayerInput
    {
        public override bool Detect()
        {
            if (base.Detect() && IsKeyPressed())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsKeyPressed()
        {
            return UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(KeyCode));
        }
    }
}
