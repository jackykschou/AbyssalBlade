using Assets.Scripts.GameScripts.Components.Input;
using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class PlayerController : GameLogic
    {
        [SerializeField]
        private AxisOnHold HorizontalAxis;

        [SerializeField]
        private AxisOnHold VerticalAxis;

        [SerializeField] 
        private ButtonOnPressed Attack1;

        protected override void Initialize()
        {
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();

            if (HorizontalAxis.Detect() || VerticalAxis.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.AxisMoved, new Vector2(HorizontalAxis.GetAxisValue(), VerticalAxis.GetAxisValue()));
            }
        }
    }
}
