using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.Editor
{
    [CustomEditor(typeof(DisableGameObjectOnTriggerCollided))]
    public class DisableGameObjectOnTriggerCollidedInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DisableGameObjectOnTriggerCollided script = (DisableGameObjectOnTriggerCollided)target;

            if (script.TargetPhysicalLayers == null)
            {
                script.TargetPhysicalLayers = new List<int>();
            }

            script.TargetPhysicalLayers.Resize(EditorGUILayout.IntField("Physical layer list size", script.TargetPhysicalLayers.Count));

            for (int i = 0; i < script.TargetPhysicalLayers.Count; ++i)
            {
                script.TargetPhysicalLayers[i] = EditorGUILayout.LayerField(script.TargetPhysicalLayers[i]);
            }
        }
    }
}
