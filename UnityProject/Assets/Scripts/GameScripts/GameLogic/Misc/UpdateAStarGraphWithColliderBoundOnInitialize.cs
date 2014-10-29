using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Misc/UpdateAStarGraphWithColliderBoundOnInitialize")]
    public class UpdateAStarGraphWithColliderBoundOnInitialize : GameLogic
    {
        private GraphUpdateObject _guo;

        protected override void Initialize()
        {
            base.Initialize();
            _guo = new GraphUpdateObject(collider.bounds) {setWalkability = false};
            AstarPath.active.UpdateGraphs(_guo);
        }

        protected override void Deinitialize()
        {
            _guo.setWalkability = true;
            AstarPath.active.UpdateGraphs(_guo);
        }
    }
}
