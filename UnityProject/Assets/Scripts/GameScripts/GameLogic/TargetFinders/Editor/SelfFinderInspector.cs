using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(SelfFinder))]
    public class SelfFinderInspector : TargetFinderInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
