using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/SpawnProjectileTowardsTarget")]
    public class SpawnProjectileTowardsTarget : TargetEffectApplier
    {
        public Prefab Prefab;

        protected override void ApplyEffect(GameObject target)
        {
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, o =>
            {
                Vector2 castDirecation = UtilityFunctions.GetDirection(GameView.CenterPosition, target.transform.position);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileTarget, target.transform);
                o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
            });
        }
    }
}
