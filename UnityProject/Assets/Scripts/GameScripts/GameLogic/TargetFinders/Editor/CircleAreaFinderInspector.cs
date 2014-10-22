using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(CircleAreaFinder))]
    public class CircleAreaFinderInspector : TargetFinderInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CircleAreaFinder finder = (CircleAreaFinder)target;

            finder.Radius = EditorGUILayout.FloatField("Radius", finder.Radius);
            finder.Radius = Mathf.Clamp(finder.Radius, 0f, float.MaxValue);
        }
    }
}
