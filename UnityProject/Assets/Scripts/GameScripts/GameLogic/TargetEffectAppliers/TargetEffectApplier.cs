using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.TargetFinders;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [RequireComponent(typeof(TargetFinder))]
    public abstract class TargetEffectApplier : GameLogic
    {
        public List<string> TargetTags = new List<string>();
        public List<int> TargetPhysicalLayers = new List<int>();
        public bool OneTimeOnlyPerTarget;

        [HideInInspector]
        public TargetFinder TargetFinder;

        private List<GameObject> _changedCache;

        public void ApplierApplyEffect(GameObject target)
        {
            if (TargetTags.Contains(target.tag) && TargetPhysicalLayers.Contains(target.layer) && (!_changedCache.Contains(target) || !OneTimeOnlyPerTarget))
            {
                _changedCache.Add(target);
                ApplyEffect(target);
            }
        }

        protected abstract void ApplyEffect(GameObject target);

        protected override void Initialize()
        {
            base.Initialize();
            _changedCache = new List<GameObject>();
            TargetFinder = GetComponent<TargetFinder>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
