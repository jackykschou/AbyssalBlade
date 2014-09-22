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

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);
            Debug.Log("OnTriggerEnter2DOnTriggerEnter2D");
            if (TagConstants.IsEnemy(gameObject.tag, coll.gameObject.tag))
            {
                GameScript s = coll.gameObject.GetComponent<GameScript>();
                if (s != null)
                {
                    s.TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount.Value);
                }
            }
            if (coll.gameObject.tag != gameObject.tag)
            {
                DisableGameObject();
            }
        }
    }
}
