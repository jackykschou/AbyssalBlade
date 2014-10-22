using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Collider2D))]
    public class SectionGateBlock : SectionLogic
    {
        public Collider2D BlockCollider;

        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.StaticObstacle);
            BlockCollider = GetComponent<Collider2D>();
            BlockCollider.isTrigger = false;
            LockGate();
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId || SectionId == (sectionId - 1))
            {
                LockGate();
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId || SectionId == (sectionId - 1))
            {
                UnLockGate();
            }
        }

        public void LockGate()
        {
            TriggerGameScriptEvent(GameScriptEvent.GateActivated);
            BlockCollider.enabled = true;
        }

        public void UnLockGate()
        {
            TriggerGameScriptEvent(GameScriptEvent.GateDeactivated);
            BlockCollider.enabled = false;
        }
    }
}
