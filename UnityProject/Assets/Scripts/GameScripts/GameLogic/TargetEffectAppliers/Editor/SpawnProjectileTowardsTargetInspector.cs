using Assets.Scripts.Constants;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(SpawnProjectileTowardsTarget))]
    public class SpawnProjectileTowardsTargetInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpawnProjectileTowardsTarget spawnProjectileTowardsTarget = (SpawnProjectileTowardsTarget)target;

            spawnProjectileTowardsTarget.Prefab = (Prefab)EditorGUILayout.EnumPopup("Prefab", spawnProjectileTowardsTarget.Prefab);
        }
    }
}
