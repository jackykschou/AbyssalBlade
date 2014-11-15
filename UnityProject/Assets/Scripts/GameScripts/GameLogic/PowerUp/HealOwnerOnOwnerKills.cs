using System;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/HealOwnerOnOwnerKills")]
    [RequireComponent(typeof(GameValueChanger))]
    public class HealOwnerOnOwnerKills : PowerUp
    {
        public Prefab EffectProjectilePrefab;
        public HealthChanger HealthChanger;

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectKills)]
        public void OnObjectKills(GameValue.GameValue gameValue, GameValueChanger gameValueChanger, float amount, bool crited)
        {
            if (EffectProjectilePrefab != Prefab.None)
            {
                PrefabManager.Instance.SpawnPrefabImmediate(EffectProjectilePrefab, gameValue.Owner.transform.position, o =>
                {
                    Vector2 castDirecation = UtilityFunctions.GetDirection(gameValue.Owner.transform.position, gameValueChanger.Owner.transform.position);
                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileTarget, gameValueChanger.Owner.transform);
                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDestination, (Vector2)gameValueChanger.Owner.transform.position);
                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueChangerOwner, gameValueChanger.Owner);
                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueOwner, gameValueChanger.Owner);

                    Action onProjectileArrival = () => HealthChanger.ApplierApplyEffect(Owner);

                    o.TriggerGameScriptEvent(GameScriptEvent.UpdateOnProjectileArrivalAction, onProjectileArrival);
                    o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
                });
            }
            else
            {
                HealthChanger.ApplierApplyEffect(Owner)
            }
        }

        protected override void Apply()
        {

        }

        protected override void UnApply()
        {
        }
    }
}
