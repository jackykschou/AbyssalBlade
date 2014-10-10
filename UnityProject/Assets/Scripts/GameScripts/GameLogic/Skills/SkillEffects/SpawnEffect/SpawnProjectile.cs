using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PrefabSpawner))]
    [RequireComponent(typeof(TargetNotNull))]
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectile")]
    public class SpawnProjectile : SkillEffect
    {
        public PrefabSpawner PrefabSpawner;
        public PositionIndicator Position;
        private Vector3 _direction;

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartSpawnProjectile();
            Activated = false;
        }

        public void StartSpawnProjectile()
        {
            ProjectileMotor motor = PrefabSpawner.SpawnPrefab(Position.Position.position).GetComponent<ProjectileMotor>();
            motor.tag = Skill.Caster.gameObject.tag;
            motor.Target = Skill.Caster.Target;
            motor.Shoot(_direction);
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdatePlayerAxis)]
        void UpdateMoveDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
