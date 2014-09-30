using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class AStarGraphUpdater : GameLogic 
    {
        protected override void Initialize()
        {
            base.Initialize();
            RescanGraph();
        }

        protected override void Deinitialize()
        {
        }

        public void UpdateGraph(Bounds bounds)
        {
            AstarPath.active.UpdateGraphs(bounds);
        }

        public void RescanGraph()
        {
            AstarPath.active.Scan();
        }
    }
}
