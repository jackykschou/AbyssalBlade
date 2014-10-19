using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(DirectionalKnockBack))]
    public class DirectionalKnockBackInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DirectionalKnockBack directionalKnockBack = (DirectionalKnockBack)target;
            directionalKnockBack.Radius = EditorGUILayout.FloatField("Radius", directionalKnockBack.Radius);
            directionalKnockBack.Radius = Mathf.Clamp(directionalKnockBack.Radius, 0f, float.MaxValue);
            directionalKnockBack.KnockBackSpeed = EditorGUILayout.FloatField("Speed", directionalKnockBack.KnockBackSpeed);
            directionalKnockBack.Time = EditorGUILayout.FloatField("Time", directionalKnockBack.Time);
            directionalKnockBack.Time = Mathf.Clamp(directionalKnockBack.Time, 0f, float.MaxValue);
        }
    }
}
