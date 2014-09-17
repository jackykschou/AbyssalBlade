using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
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

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (TagConstants.IsEnemy(gameObject.tag, coll.gameObject.tag))
            {
                GameScript s = coll.gameObject.GetComponent<GameScript>();
                if (s != null)
                {
                    s.TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount);
                    DisableGameObject();
                }
            }
        }
    }
}
