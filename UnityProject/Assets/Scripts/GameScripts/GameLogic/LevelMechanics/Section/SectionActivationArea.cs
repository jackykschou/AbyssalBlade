using Assets.Scripts.Constants;
using UnityEngine;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionActivationArea")]
    [RequireComponent(typeof(Collider2D))]
    public class SectionActivationArea : SectionLogic 
    {
        public Collider2D ActivationArea;

        protected override void Initialize()
        {
            base.Initialize();
            ActivationArea = GetComponent<Collider2D>();
            ActivationArea.isTrigger = true;
            gameObject.layer = LayerConstants.LayerMask.SectionActivateArea;
            ActivationArea.enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);
            TriggerGameEvent(GameEvent.OnSectionActivated, SectionId);
            ActivationArea.enabled = false;
        }

        [GameEventAttribute(GameEvent.OnSectionActivated)]
        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                ActivationArea.enabled = false;
            }
        }
    }
}
