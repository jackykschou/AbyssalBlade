using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health.Editor
{
    [CustomEditor(typeof(HealthChanger))]
    public class HealthChangerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            HealthChanger healthChanger = (HealthChanger)target;

            healthChanger.HealthChangeType = (HealthChangeType)EditorGUILayout.EnumPopup("HealthChangeType", healthChanger.HealthChangeType);
            healthChanger.OneTimeOnlyPerTarget = EditorGUILayout.Toggle("OneTimeOnlyPerTarget", healthChanger.OneTimeOnlyPerTarget);

            if (healthChanger.TargetTags == null)
            {
                healthChanger.TargetTags = new List<string>();
            }

            healthChanger.TargetTags.Resize(EditorGUILayout.IntField("Target tag list size", healthChanger.TargetTags.Count));

            for (int i = 0; i < healthChanger.TargetTags.Count; ++i)
            {
                healthChanger.TargetTags[i] = EditorGUILayout.TagField(healthChanger.TargetTags[i]);
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
