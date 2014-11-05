using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof (ChangeHealthChangerDamageCriticalChance))]
    public class ChangeHealthChangerDamageCriticalChanceInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ChangeHealthChangerDamageCriticalChance changeHealthChangerDamageCriticalChance =
                (ChangeHealthChangerDamageCriticalChance) target;
            changeHealthChangerDamageCriticalChance.ChangeAmount = EditorGUILayout.FloatField("Change Amount", changeHealthChangerDamageCriticalChance.ChangeAmount);
        }
    }
}
