using Assets.Scripts.Managers;
using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class SpawnPrefabSkillEffect : SkillEffect
    {

        public Prefab Prefab;
        public Transform Position;

        public override void Activate()
        {
            base.Activate();
            SpawnPrefab();
            Activated = false;
        }

        public void SpawnPrefab()
        {
            if (Position == null)
            {
                Position = transform;
            }
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, Position.position);
        }
    }
}