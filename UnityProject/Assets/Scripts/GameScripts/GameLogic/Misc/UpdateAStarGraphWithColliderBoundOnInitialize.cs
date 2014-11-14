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

        //[GameScriptEvent(Constants.GameScriptEvent.GateActivated)]
        [GameEvent(Constants.GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            if(_guo == null)
                _guo = new GraphUpdateObject(collider2D.bounds);
            _guo.setWalkability = false;
            AstarPath.active.UpdateGraphs(_guo);
            //AstarPath.active.Scan();
        }

        //[GameScriptEvent(Constants.GameScriptEvent.GateDeactivated)]
        [GameEvent(Constants.GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            
            _guo.setWalkability = true;
            AstarPath.active.UpdateGraphs(_guo);
            //AstarPath.active.Scan();
        }
        
    }
}
