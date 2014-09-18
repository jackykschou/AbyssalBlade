using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class AxisOnHold : PlayerInput
    {
        private const float _axisValueThreshold = 0.1f;

        public float GetAxisValue()
        {
            return UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(KeyCode));
        }

        public override bool Detect()
        {
            if (base.Detect() && IsAxisOnHold())
            {
                return true;
            }
            return false;
        }

        private bool IsAxisOnHold()
        {
            return Mathf.Abs(UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(KeyCode))) > _axisValueThreshold;
        }
    }
}
