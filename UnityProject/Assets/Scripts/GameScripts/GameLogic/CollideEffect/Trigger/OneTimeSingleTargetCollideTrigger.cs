using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Trigger
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("CollideEffectTrigger/OneTimeSingleTargetCollideTrigger")]
    public class OneTimeSingleTargetCollideTrigger : GameLogic
    {
        public Collider2D Collider;

        protected override void Initialize()
        {
            base.Initialize();
            Collider = GetComponent<Collider2D>();
            Collider.enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);

            if(coll.gameObject.tag != gameObject.tag)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnCollideTriggerTriggered, coll.gameObject);
                ImmediateDisableGameObject();
                Collider.enabled = false;
            }
        }
    }
}
