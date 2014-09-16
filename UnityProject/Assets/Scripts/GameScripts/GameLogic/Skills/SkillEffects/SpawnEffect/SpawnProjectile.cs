using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile;
using Assets.Scripts.Managers;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    public class SpawnProjectile : SkillEffect
    {
        public Prefab ProjectilePrefab;
        public PositionIndicator position;

        [GameScriptEventAttribute(GameScriptEvent.SkillCastTriggerSucceed)]
        public void Spawn()
        {
            ProjectileMotor motor = PrefabManager.Instance.SpawnPrefab(ProjectilePrefab, position.Position.position).GetComponent<ProjectileMotor>();
            motor.tag = SKill.gameObject.tag;
            motor.Target = SKill.Caster.Target.position;
            motor.Shoot();
        }
    }
}
