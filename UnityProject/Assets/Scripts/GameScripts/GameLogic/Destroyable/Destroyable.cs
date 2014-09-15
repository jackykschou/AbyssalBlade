using Assets.Scripts.GameScripts.Components.GameValue;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Destroyable
{
    public class Destroyable : GameLogic
    {
        public bool Invincible;
        public GameValue HitPoint;
        public GameValue DamageReduction;

        [GameLogicEventAttribute(GameLogicEvent.DamageTaked)]
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
                TriggerGameLogicEvent(GameLogicEvent.OnObjectHaveNoHealth);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
