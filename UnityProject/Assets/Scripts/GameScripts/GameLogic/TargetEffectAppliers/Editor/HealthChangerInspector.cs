using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(HealthChanger))]
    public class HealthChangerInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HealthChanger healthChanger = (HealthChanger)target;

            healthChanger.GameValueChanger = EditorGUILayout.ObjectField("GameValueChanger", healthChanger.GameValueChanger, typeof(GameValueChanger)) as GameValueChanger;
        }
    }
}