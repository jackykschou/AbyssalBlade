using Assets.Scripts.GameScripts.GameLogic.TargetFinders;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class FindTarget : SkillEffect
    {
        public TargetFinder TargetFinder;

        public override void Activate()
        {
            base.Activate();
            TargetFinder.FindAndApply();
            Activated = false;
        }
    }
}