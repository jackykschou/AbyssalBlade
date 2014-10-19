using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/SingleRayCastFinder")]
    public class SingleRayCastFinder : TargetFinder
    {
        public float Range;
        public float RayAngleRandomness;

        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
            ClearTargets();
            Vector2 castDirecation = Quaternion.AngleAxis(Random.Range(-Range, Range), Vector3.forward) * FinderPosition.Direction;
            string[] layers = TargetPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
            int mask = LayerMask.GetMask(layers);
            RaycastHit2D raycast = Physics2D.Raycast(FinderPosition.Position.position, castDirecation, Range, mask);
            if (raycast.collider != null)
            {
                AddTarget(raycast.collider.gameObject);
            }
        }
    }
}
