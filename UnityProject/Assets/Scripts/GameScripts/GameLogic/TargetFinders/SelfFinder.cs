using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/SelfFinder")]
    public class SelfFinder : TargetFinder 
    {
        protected override void Initialize()
        {
            base.Initialize();
            FindTargets();
        }

        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
            Targets.Add(transform.root.gameObject);
        }
    }
}
