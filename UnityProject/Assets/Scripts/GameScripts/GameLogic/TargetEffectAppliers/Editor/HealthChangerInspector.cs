using Assets.Scripts.Constants;
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

            healthChanger.HealthChangeType = (HealthChangeType)EditorGUILayout.EnumPopup("HealthChangeType", healthChanger.HealthChangeType);
            healthChanger.CriticalChance = EditorGUILayout.FloatField("Critical Chance", healthChanger.CriticalChance);
            healthChanger.CriticalDamagePercentage = EditorGUILayout.FloatField("Critical Damage Percentage", healthChanger.CriticalDamagePercentage);
            healthChanger.CriticalChance = Mathf.Clamp(healthChanger.CriticalChance, 0f, 1f);
            healthChanger.CriticalDamagePercentage = Mathf.Clamp(healthChanger.CriticalDamagePercentage, 0f, 10f);

            if (healthChanger.Amount == null)
            {
                return;
            }

            switch (healthChanger.HealthChangeType)
            {
                case HealthChangeType.Fixed:
                    healthChanger.Amount.InitialMinValue = EditorGUILayout.FloatField("Amount Min Value", healthChanger.Amount.InitialMinValue);
                    healthChanger.Amount.InitialMaxValue = EditorGUILayout.FloatField("Amount Max Value", healthChanger.Amount.InitialMaxValue);
                    healthChanger.Amount.InitialValue = EditorGUILayout.FloatField("Amount Initial Value", healthChanger.Amount.InitialValue);
                    break;
                case HealthChangeType.CurrentPercentage:
                case HealthChangeType.MaxPercentage:
                    healthChanger.Percentage = EditorGUILayout.FloatField("Percentage", healthChanger.Percentage);
                    healthChanger.Percentage = Mathf.Clamp(healthChanger.Percentage, 0f, 1f);
                    break;
                case HealthChangeType.FixedPerSecond:
                    healthChanger.Amount.InitialMinValue = EditorGUILayout.FloatField("Amount Min Value", healthChanger.Amount.InitialMinValue);
                    healthChanger.Amount.InitialMaxValue = EditorGUILayout.FloatField("Amount Max Value", healthChanger.Amount.InitialMaxValue);
                    healthChanger.Amount.InitialValue = EditorGUILayout.FloatField("Amount Initial Value", healthChanger.Amount.InitialValue);
                    healthChanger.Duration = EditorGUILayout.IntField("Duration", healthChanger.Duration);
                    healthChanger.Duration = Mathf.Clamp(healthChanger.Duration, 0, int.MaxValue);
                    healthChanger.Stackable = EditorGUILayout.Toggle("Stackable", healthChanger.Stackable);
                    if (!healthChanger.Stackable)
                    {
                        healthChanger.NonStackableLabel = (HealthModifierNonStackableLabel)EditorGUILayout.EnumPopup("Non Stackable Label", healthChanger.NonStackableLabel);
                    }
                    break;
                case HealthChangeType.CurrentPercentagePerSecond:
                case HealthChangeType.MaxPercentagePerSecond:
                    healthChanger.Percentage = EditorGUILayout.FloatField("Percentage", healthChanger.Percentage);
                    healthChanger.Duration = EditorGUILayout.IntField("Duration", healthChanger.Duration);
                    healthChanger.Duration = Mathf.Clamp(healthChanger.Duration, 0, int.MaxValue);
                    healthChanger.Stackable = EditorGUILayout.Toggle("Stackable", healthChanger.Stackable);
                    if (!healthChanger.Stackable)
                    {
                        healthChanger.NonStackableLabel = (HealthModifierNonStackableLabel)EditorGUILayout.EnumPopup("Non Stackable Label", healthChanger.NonStackableLabel);
                    }
                    break;
            }
        }
    }
}
