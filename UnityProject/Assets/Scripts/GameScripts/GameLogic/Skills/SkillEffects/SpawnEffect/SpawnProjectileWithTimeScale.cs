using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectileWithTimeScale")]
    public class SpawnProjectileWithTimeScale : SkillEffect
    {
        public PrefabSpawner PrefabSpawner;
        public PositionIndicator Position;
        private float _time;

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            _time = 0f;
        }

        public override void Activate()
        {
            base.Activate();
            StartSpawnProjectile();
            Activated = false;
        }

        public void StartSpawnProjectile()
        {
            PrefabSpawner.SpawnPrefabImmediate(Position.Position.position, o =>
            {
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime, _time);
                ProjectileMotor motor = o.GetComponent<ProjectileMotor>();
                motor.tag = Skill.Caster.gameObject.tag;
                motor.Target = Skill.Caster.Target;
                motor.Shoot(Skill.Caster.PointingDirection);
            });
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            _time = time;
        }
    }
}
