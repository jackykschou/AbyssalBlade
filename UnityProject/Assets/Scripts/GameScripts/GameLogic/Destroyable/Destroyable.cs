using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Destroyable
{
    public class Destroyable : GameLogic
    {
        public bool Invincible;
        public GameValue HitPoint;
        public GameValue DamageReduction;

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage)
        {
            if (Invincible)
            {
                return;
            }

            if (DamageReduction >= 1.0f)
            {
                HitPoint -= 1.0f;
            }

            HitPoint -= (DamageReduction * damage);

            if (HitPoint <= 0f)
            {
                TriggerGameScriptEvent(Constants.GameScriptEvent.OnDestroyableDestroyed);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
