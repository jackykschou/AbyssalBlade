using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/Health")]
    [RequireComponent(typeof(GameValue.GameValue))]
    public class Health : GameLogic
    {
        public bool Invincible;

        public GameValue.GameValue HitPoint;

        public bool HitPointAtZero { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            HitPointAtZero = false;
            Invincible = false;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeHealth)]
        public virtual void ChangeHealthFixed(GameValueChanger healthChanger)
        {
            if ((Invincible && healthChanger.RawAmount < 0f) || HitPointAtZero || Mathf.Approximately(0f, healthChanger.RawAmount))
            {
                return;
            }
            HitPoint.ChangeGameValue(healthChanger);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void OnObjectHasNoHitPoint()
        {
            HitPointAtZero = true;
            HitPoint.Value = 0f;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged)]
        public void OnHealthCurrentValueChanged(GameValue.GameValue health, float changedAmount, bool crited)
        {
            if (health != HitPoint)
            {
                return;
            }

            if (changedAmount <= 0f)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, Mathf.Abs(changedAmount), crited);
            }
            else
            {
                TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeHeal, Mathf.Abs(changedAmount));
            }

            if (HitPoint <= 0f)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint);
            }
        }
    
        protected override void Deinitialize()
        {
        }
    }
}
