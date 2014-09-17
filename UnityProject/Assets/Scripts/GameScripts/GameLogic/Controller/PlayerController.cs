using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Controller
{
    public class PlayerController : GameLogic
    {
        [SerializeField]
        private AxisOnHold HorizontalAxis;

        [SerializeField] 
        private AxisOnHold VerticalAxis;

        [SerializeField]
        private AxisOnHold JoyStickVerticalAxis;

        [SerializeField]
        private AxisOnHold JoyStickHorizontalAxis;

        [SerializeField] 
        private ButtonOnPressed Attack1;

        [SerializeField]
        private ButtonOnPressed Attack2;

        [SerializeField]
        private ButtonOnPressed Attack3;

        [SerializeField]
        private ButtonOnPressed Attack4;

        protected override void Initialize()
        {
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();

            if (Attack1.Detect())
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack1ButtonPressed);
            }
            else if (Attack2.Detect())
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack2ButtonPressed);
            }
            else if (Attack3.Detect())
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack3ButtonPressed);
            }
            else if (Attack4.Detect())
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack4ButtonPressed);
            }
            else if (HorizontalAxis.Detect() || JoyStickVerticalAxis.Detect() || VerticalAxis.Detect() || JoyStickHorizontalAxis.Detect())
            {
                const float axisThreshHold = 0.2f;

                float horizontalMagnitude = Mathf.Max(Mathf.Abs(HorizontalAxis.GetAxisValue()), Mathf.Abs(JoyStickHorizontalAxis.GetAxisValue()));
                float verticalMagnitude = Mathf.Max(Mathf.Abs(VerticalAxis.GetAxisValue()), Mathf.Abs(JoyStickVerticalAxis.GetAxisValue()));

                if (horizontalMagnitude > axisThreshHold || verticalMagnitude > axisThreshHold)
                {
                    float horizontalValue = Mathf.Abs(HorizontalAxis.GetAxisValue()) >
                                        Mathf.Abs(JoyStickHorizontalAxis.GetAxisValue())
                    ? HorizontalAxis.GetAxisValue()
                    : JoyStickHorizontalAxis.GetAxisValue();

                    float verticalValue = Mathf.Abs(VerticalAxis.GetAxisValue()) >
                                        Mathf.Abs(JoyStickVerticalAxis.GetAxisValue())
                    ? HorizontalAxis.GetAxisValue()
                    : JoyStickHorizontalAxis.GetAxisValue();

                    TriggerGameScriptEvent(GameScriptEvent.PlayerAxisMoved, new Vector2(horizontalValue, verticalValue));
                }
            }
        }
    }
}
