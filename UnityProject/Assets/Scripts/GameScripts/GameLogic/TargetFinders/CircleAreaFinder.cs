using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/CircleAreaFinder")]
    public class CircleAreaFinder : TargetFinder 
    {
        public float Radius;

        protected override void FindTargets()
        {
            ClearTargets();
            string[] layers = TargetPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
            int mask = LayerMask.GetMask(layers);
            foreach (var col in Physics2D.OverlapCircleAll(FinderPosition.Position.position, Radius, mask))
            {
                AddTarget(col.gameObject);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
