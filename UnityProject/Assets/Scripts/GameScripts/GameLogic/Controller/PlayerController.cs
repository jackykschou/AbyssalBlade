using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

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

        [SerializeField] 
        private ButtonOnPressed Dash;

        private bool _skill1Enabled;
        private bool _skill2Enabled;
        private bool _skill3Enabled;
        private bool _skill4Enabled;

        protected override void Initialize()
        {
            base.Initialize();
            _skill1Enabled = true;
            _skill2Enabled = true;
            _skill3Enabled = true;
            _skill4Enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (gameObject.HitPointAtZero())
            {
                return;
            }

            if ((HorizontalAxis.Detect() || JoyStickVerticalAxis.Detect() || VerticalAxis.Detect() || JoyStickHorizontalAxis.Detect()))
            {
                float horizontalValue = Mathf.Abs(HorizontalAxis.GetAxisValue()) >
                                    Mathf.Abs(JoyStickHorizontalAxis.GetAxisValue())
                ? HorizontalAxis.GetAxisValue()
                : JoyStickHorizontalAxis.GetAxisValue();

                float verticalValue = Mathf.Abs(VerticalAxis.GetAxisValue()) >
                                    Mathf.Abs(JoyStickVerticalAxis.GetAxisValue())
                ? VerticalAxis.GetAxisValue()
                : JoyStickVerticalAxis.GetAxisValue();

                Vector2 direction = new Vector2(horizontalValue, verticalValue);

                TriggerGameScriptEvent(GameScriptEvent.PlayerAxisMoved, direction);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (Attack1.Detect() && _skill1Enabled)
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack1ButtonPressed);
            }
            else if (Attack2.Detect() && _skill2Enabled)
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack2ButtonPressed);
            }
            else if (Attack3.Detect() && _skill3Enabled)
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack3ButtonPressed);
            }
            else if (Attack4.Detect() && _skill4Enabled)
            {
                TriggerGameScriptEvent(GameScriptEvent.PlayerAttack4ButtonPressed);
            }
            else if (Dash.Detect())
            {
                TriggerGameEvent(GameEvent.OnPlayerDashButtonPressed);
                TriggerGameScriptEvent(GameScriptEvent.PlayerDashButtonPressed);
            }
        }

        [GameEventAttribute(GameEvent.EnableAbility)]
        public void EnableAbility(int id)
        {
            switch (id)
            {
                case 1:
                    _skill1Enabled = true;
                    break;
                case 2:
                    _skill2Enabled = true;
                    break;
                case 3:
                    _skill3Enabled = true;
                    break;
                case 4:
                    _skill4Enabled = true;
                    break;
            }
        }

        [GameEventAttribute(GameEvent.DisableAbility)]
        public void DisableAbility(int id)
        {
            switch (id)
            {
                case 1:
                    _skill1Enabled = false;
                    break;
                case 2:
                    _skill2Enabled = false;
                    break;
                case 3:
                    _skill3Enabled = false;
                    break;
                case 4:
                    _skill4Enabled = false;
                    break;
            }
        }
    }
}
