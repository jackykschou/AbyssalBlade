using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.Managers;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(TargetNotNull))]
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectile")]
    public class SpawnProjectile : SkillEffect
    {
        public Prefab ProjectilePrefab;
        public PositionIndicator Position;

        public override void Activate()
        {
            base.Activate();
            StartSpawnProjectile();
            Activated = false;
        }

        public void StartSpawnProjectile()
        {
            ProjectileMotor motor = PrefabManager.Instance.SpawnPrefab(ProjectilePrefab, Position.Position.position).GetComponent<ProjectileMotor>();
            motor.tag = SKill.Caster.gameObject.tag;
            motor.Target = SKill.Caster.Target.position;
            motor.Shoot();
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
