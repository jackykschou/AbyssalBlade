using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("Skill/SkillEffect/SpawnPrefab")]
    public class SpawnPrefab : SkillEffect 
    {
        public PrefabSpawner PrefabSpawner;

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
            StartSpawn();
            Activated = false;
        }

        public void StartSpawn()
        {
            PrefabSpawner.SpawnPrefab();
        }
    }
}
