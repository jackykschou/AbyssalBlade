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
        public bool Destroyed { get; private set; }

        [Range(0f, float.MaxValue)] 
        [SerializeField]
        private float _delay = 1.5f;

        protected override void Initialize()
        {
            base.Initialize();
            Destroyed = false;
            Invincible = false;
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage)
        {
            if (Invincible || Destroyed)
            {
                return;
            }

            if (DamageReduction >= 1.0f)
            {
                HitPoint -= 1.0f;
            }

            HitPoint -= ((1 - DamageReduction) * damage);

            if (HitPoint <= 0f)
            {
                Destroyed = true;
                TriggerGameScriptEvent(Constants.GameScriptEvent.OnObjectDestroyed);
                DisableGameObject(_delay);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
