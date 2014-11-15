using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/SingleRayCastFinder")]
    public class SingleRayCastFinder : TargetFinder
    {
        public float Range;
        public float RayAngleRandomness;

        public Prefab ProjectilePrefab;

        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
            ClearTargets();
            Vector2 castDirecation = Quaternion.AngleAxis(Random.Range(-RayAngleRandomness, RayAngleRandomness), Vector3.forward) * FinderPosition.Direction;
            string[] layers = TargetPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
            int mask = LayerMask.GetMask(layers);
            RaycastHit2D raycast = Physics2D.Raycast(FinderPosition.Position.position, castDirecation, Range, mask);
            PrefabManager.Instance.SpawnPrefabImmediate(ProjectilePrefab, FinderPosition.Position.position, o =>
            {
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDestination, raycast.collider != null ?
                    (Vector2)(FinderPosition.Position.position + (Vector3)(castDirecation * Vector2.Distance(raycast.collider.transform.position, FinderPosition.Position.position))) :
                    (Vector2)(FinderPosition.Position.position + (new Vector3(castDirecation.x, castDirecation.y, 0) * 100f)));
                o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
            });
            if (raycast.collider != null)
            {
                AddTarget(raycast.collider.gameObject);
            }
        }
    }
}
