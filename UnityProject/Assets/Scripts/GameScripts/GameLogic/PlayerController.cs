﻿using Assets.Scripts.GameScripts.Components.Input;
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

            if (HorizontalAxis.Detect() || VerticalAxis.Detect())
            {
                TriggerGameLogicEvent(GameLogicEvent.AxisMoved, new Vector2(HorizontalAxis.GetAxisValue(), VerticalAxis.GetAxisValue()));
            }
        }
    }
}
