using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [RequireComponent(typeof(PositionIndicator))]
    public abstract class TargetFinder : GameLogic
    {
        public List<string> TargetTags = new List<string>();
        public List<int> TargetPhysicalLayers = new List<int>();
        public PositionIndicator FinderPosition;
        public List<TargetEffectApplier> TargetEffectAppliers;

        public List<GameObject> Targets;

        public void FindAndApply()
        {
            FindTargets();
            ApplyEffects();
        }

        protected abstract void FindTargets();

        protected override void Initialize()
        {
            base.Initialize();
            ClearTargets();
            TargetEffectAppliers.ForEach(a => a.TargetFinder = this);
        }

        public void ClearTargets()
        {
            Targets = new List<GameObject>();
        }

        protected void ApplyEffects()
        {
            TargetEffectAppliers.ForEach(c => Targets.ForEach(c.ApplierApplyEffect));
        }

        public void AddTarget(GameObject target)
        {
            if (TargetTags.Contains(target.tag) && TargetPhysicalLayers.Contains(target.layer) && !Targets.Contains(target) && !target.HitPointAtZero())
            {
                Targets.Add(target);
            }
        }
    }
}
