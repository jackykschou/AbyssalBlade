using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(ChangeDamageReduction))]
    public class ChangeDamageReductionInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ChangeDamageReduction changeDamageReduction = (ChangeDamageReduction)target;
            changeDamageReduction.ChangeAmount = EditorGUILayout.FloatField("Change Amount", changeDamageReduction.ChangeAmount);
        }
    }
}
