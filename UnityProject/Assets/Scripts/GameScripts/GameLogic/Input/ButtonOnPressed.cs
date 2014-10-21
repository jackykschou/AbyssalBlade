using System.Linq;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Input
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
            return KeyCodes.Any(c => UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(c)));
        }
    }
}
