using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Health;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.ScaleWithTime
{
    [RequireComponent(typeof(HealthChanger))]
    public class HealthChangerAmountScaleWithTime : GameLogic
    {
        [Range(0f, 10f)]
        public float MinTime;
        [Range(0f, 10f)]
        public float MaxTime;

        private HealthChanger _healthChanger;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            if (time < MinTime)
            {
                _healthChanger.Amount.Value = 0f;
                _healthChanger.Percentage = 0f;
            }
            else if (time >= MaxTime)
            {
                return;
            }
            else
            {
                float scale = time / MaxTime;
                _healthChanger.Amount.Value = scale * _healthChanger.Amount.Value;
                _healthChanger.Percentage = scale * _healthChanger.Percentage;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _healthChanger = GetComponent<HealthChanger>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
