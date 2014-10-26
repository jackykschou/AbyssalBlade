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
        public bool HasApplyCoolDown;
        public float ApplyCooldown;
        public bool OneTimeOnlyPerTarget;

        [HideInInspector]
        public TargetFinder TargetFinder;

        private Dictionary<GameObject, float> _changedCache;

        public void ApplierApplyEffect(GameObject target)
        {
            if (TargetTags.Contains(target.tag) && TargetPhysicalLayers.Contains(target.layer))
            {
                if(_changedCache.ContainsKey(target))
                {
                    if (OneTimeOnlyPerTarget)
                    {
                        return;
                    }
                    if (HasApplyCoolDown && (Time.time - _changedCache[target]) < ApplyCooldown)
                    {
                        return;
                    }
                }
                else
                {
                    _changedCache.Add(target, 0f);
                }
                _changedCache[target] = Time.time;
                ApplyEffect(target);
            }
        }

        protected abstract void ApplyEffect(GameObject target);

        protected override void Initialize()
        {
            base.Initialize();
            _changedCache = new Dictionary<GameObject, float>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
