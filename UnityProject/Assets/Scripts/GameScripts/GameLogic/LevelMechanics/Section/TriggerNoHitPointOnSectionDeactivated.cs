using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    public class TriggerNoHitPointOnSectionDeactivated : SectionLogic
    {
        private bool _triggered;
        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId && !_triggered)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DecrementSectionEnemy()
        {
            _triggered = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _triggered = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
