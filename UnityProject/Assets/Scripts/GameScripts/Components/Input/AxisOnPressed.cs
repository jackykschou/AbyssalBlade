using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.Components.Input
{
    [System.Serializable]
    public class AxisOnPressed : ButtonOnPressed 
    {
        public float GetAxisValue()
        {
            return UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(KeyCode));
        }
    }
}
