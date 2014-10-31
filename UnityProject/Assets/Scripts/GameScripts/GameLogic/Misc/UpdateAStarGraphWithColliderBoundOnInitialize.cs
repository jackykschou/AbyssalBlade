using Assets.Scripts.Attributes;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Misc/UpdateAStarGraphWithColliderBoundOnInitialize")]
    public class UpdateAStarGraphWithColliderBoundOnInitialize : GameLogic
    {
        private GraphUpdateObject _guo;

        protected override void Deinitialize()
        {
        }

        [GameEvent(Constants.GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            _guo = new GraphUpdateObject(collider2D.bounds) { setWalkability = false };
            AstarPath.active.UpdateGraphs(_guo);
        }

        [GameEvent(Constants.GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            _guo.setWalkability = true;
            AstarPath.active.UpdateGraphs(_guo);
        }
    }
}
