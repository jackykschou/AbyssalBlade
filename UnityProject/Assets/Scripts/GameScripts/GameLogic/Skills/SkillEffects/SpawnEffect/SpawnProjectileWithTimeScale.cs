﻿using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PositionIndicator))]
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectileWithTimeScale")]
    public class SpawnProjectileWithTimeScale : SkillEffect
    {
        public PrefabSpawner PrefabSpawner;
        public PositionIndicator Position;
        public float RayAngleRandomness;

        private float _time;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
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
                Vector2 castDirecation = Quaternion.AngleAxis(Random.Range(-RayAngleRandomness, RayAngleRandomness), Vector3.forward) * Skill.Caster.PointingDirection;
                motor.Shoot(castDirecation);
            });
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            _time = time;
        }
    }
}
