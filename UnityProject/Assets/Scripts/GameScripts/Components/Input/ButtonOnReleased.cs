using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class ButtonOnReleased : PlayerInput
    {
        public override bool Detect()
        {
            if (base.Detect() && IsKeyReleased())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsKeyReleased()
        {
            return UnityEngine.Input.GetButtonUp(InputConstants.GetKeyCodeName(KeyCode));
        }
    }
}
