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

        protected override void Initialize()
        {
            base.Initialize();
            if (_guo == null)
            {
                _guo = new GraphUpdateObject(collider2D.bounds);
            }
        }

        [GameEvent(Constants.GameEvent.OnLevelStarted)]
        [GameEvent(Constants.GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            _guo.setWalkability = !collider2D.enabled;
            AstarPath.active.UpdateGraphs(_guo);
        }

        [GameScriptEvent(Constants.GameScriptEvent.GateActivated)]
        public void GateActivated()
        {
            _guo.setWalkability = false;
            AstarPath.active.UpdateGraphs(_guo);
        }

        [GameScriptEvent(Constants.GameScriptEvent.GateDeactivated)]
        [GameEvent(Constants.GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            _guo.setWalkability = true;
            AstarPath.active.UpdateGraphs(_guo);
        }
        
    }
}
