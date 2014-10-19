using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(CollideWithTriggerFinder))]
    public class CollideWithTriggerFinderInspector : TargetFinderInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
