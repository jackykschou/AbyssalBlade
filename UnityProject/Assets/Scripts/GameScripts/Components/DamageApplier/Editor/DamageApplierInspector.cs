using Assets.Scripts.Constants;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.DamageApplier.Editor
{
    [CustomEditor(typeof(DamageApplier))]
    public class DamageApplierInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DamageApplier damageApplier = (DamageApplier)target;

            damageApplier.DamageType = (DamageApplyType)EditorGUILayout.EnumPopup("DamageType", damageApplier.DamageType);
            damageApplier.OneTimeOnlyPerTarget = EditorGUILayout.Toggle("OneTimeOnlyPerTarget", damageApplier.OneTimeOnlyPerTarget);
            damageApplier.Stackable = EditorGUILayout.Toggle("Stackable", damageApplier.Stackable);

            switch (damageApplier.DamageType)
            {
                case DamageApplyType.Fixed:
                    damageApplier.Amount.InitialMinValue = EditorGUILayout.FloatField("Amount Min Value", damageApplier.Amount.InitialMinValue);
                    damageApplier.Amount.InitialMaxValue = EditorGUILayout.FloatField("Amount Max Value", damageApplier.Amount.InitialMaxValue);
                    damageApplier.Amount.InitialValue = EditorGUILayout.FloatField("Amount Initial Value", damageApplier.Amount.InitialValue);
                    break;
                case DamageApplyType.CurrentPercentage:
                case DamageApplyType.MaxPercentage:
                    damageApplier.Percentage = EditorGUILayout.FloatField("Percentage", damageApplier.Percentage);
                    damageApplier.Percentage = Mathf.Clamp(damageApplier.Percentage, 0f, 1f);
                    break;
                case DamageApplyType.FixedPerSecond:
                    damageApplier.Amount.InitialMinValue = EditorGUILayout.FloatField("Amount Min Value", damageApplier.Amount.InitialMinValue);
                    damageApplier.Amount.InitialMaxValue = EditorGUILayout.FloatField("Amount Max Value", damageApplier.Amount.InitialMaxValue);
                    damageApplier.Amount.InitialValue = EditorGUILayout.FloatField("Amount Initial Value", damageApplier.Amount.InitialValue);
                    damageApplier.Duration = EditorGUILayout.IntField("Duration", damageApplier.Duration);
                    damageApplier.Duration = Mathf.Clamp(damageApplier.Duration, 0, int.MaxValue);
                    break;
                case DamageApplyType.CurrentPercentagePerSecond:
                case DamageApplyType.MaxPercentagePerSecond:
                    damageApplier.Percentage = EditorGUILayout.FloatField("Percentage", damageApplier.Percentage);
                    damageApplier.Duration = EditorGUILayout.IntField("Duration", damageApplier.Duration);
                    damageApplier.Duration = Mathf.Clamp(damageApplier.Duration, 0, int.MaxValue);
                    break;
            }
        }
    }
}
