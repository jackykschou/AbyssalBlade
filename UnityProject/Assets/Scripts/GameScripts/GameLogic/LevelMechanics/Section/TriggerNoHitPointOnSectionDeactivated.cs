using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    public class TriggerNoHitPointOnSectionDeactivated : SectionLogic 
    {
        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
