using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("Skill/SkillEffect/TargetFinder/CircleAreaFinder")]
    public class CircleAreaFinder : TargetFinder 
    {
        public float Radius;

        protected override void FindTargets()
        {
            ClearTargets();
            foreach (var col in Physics2D.OverlapCircleAll(FinderPosition.Position.position, Radius))
            {
                AddTarget(col.gameObject);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
