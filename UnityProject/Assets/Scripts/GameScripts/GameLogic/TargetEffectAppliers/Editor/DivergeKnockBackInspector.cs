using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(DivergeKnockBack))]
    public class DivergeKnockBackInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DivergeKnockBack directionalKnockBack = (DivergeKnockBack)target;
            directionalKnockBack.KnockBackSpeed = EditorGUILayout.FloatField("Speed", directionalKnockBack.KnockBackSpeed);
            directionalKnockBack.Time = EditorGUILayout.FloatField("Time", directionalKnockBack.Time);
            directionalKnockBack.Time = Mathf.Clamp(directionalKnockBack.Time, 0f, float.MaxValue);
        }
    }
}
