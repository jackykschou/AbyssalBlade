using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.DamageApplier
{
    public class DamageApplier : GameScriptComponent
    {
        public bool Stackable;
        public DamageApplyType DamageType;

        private List<GameObject> _damagedCache; 

        public void ApplyDamage(GameObject target)
        {
            if (TagConstants.IsEnemy(GameScript.gameObject.tag, target.tag) && !target.IsDestroyed() &&
                (!_damagedCache.Contains(target) || Stackable))
            {
                ApplyDamageHelper(target);
            }
        }

        private void ApplyDamageHelper(GameObject target)
        {
            _damagedCache.Add(target);
            switch (DamageType)
            {
                case DamageApplyType.Fixed:
                    break;
                case DamageApplyType.CurrentPercentage:
                    break;
                case DamageApplyType.MaxPercentage:
                    break;
                case DamageApplyType.PerSecondFixed:
                    break;
                case DamageApplyType.PerSecondCurrentPercentage:
                    break;
                case DamageApplyType.PerSecondMaxPercentage:
                    break;
            }
        }

        public override void Initialize()
        {
            _damagedCache = new List<GameObject>();
        }

        public override void Deinitialize()
        {
        }
    }
}
