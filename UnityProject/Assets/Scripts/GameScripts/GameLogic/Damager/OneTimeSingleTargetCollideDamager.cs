using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Damager
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Skill/Damager/CollideDamager/OneTimeSingleTargetCollideDamager")]
    public class OneTimeSingleTargetCollideDamager : GameLogic
    {
        public GameValue DamageAmount;

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);

            if (TagConstants.IsEnemy(gameObject.tag, coll.gameObject.tag))
            {
                GameScript s = coll.gameObject.GetComponent<GameScript>();
                if (s != null)
                {
                    s.TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount.Value);
                    DisableGameObject();
                    return;
                }
            }
            if (coll.gameObject.tag != gameObject.tag && !coll.gameObject.IsDestroyed())
            {
                DisableGameObject();
            }
        }
    }
}
