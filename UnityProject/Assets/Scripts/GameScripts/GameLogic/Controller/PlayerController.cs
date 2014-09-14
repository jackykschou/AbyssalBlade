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
                TriggerGameLogicEvent(GameLogicEvent.PlayerAttack1ButtonPressed);
            }
            else if (Attack2.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.PlayerAttack2ButtonPressed);
            }
            else if (Attack3.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.PlayerAttack3ButtonPressed);
            }
            else if (Attack4.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.PlayerAttack4ButtonPressed);
            }
            else if (HorizontalAxis.Detect() || VerticalAxis.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.AxisMoved, new Vector2(HorizontalAxis.GetAxisValue(), VerticalAxis.GetAxisValue()));
            }
        }
    }
}
