using UnityEditor;

namespace Assets.Scripts.Managers.Editor
{
    [CustomEditor(typeof(PrefabManager))]
    public class PrefabManagerInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            PrefabManager manager = (PrefabManager)target;

            DrawDefaultInspector();

            //if (GUILayout.Button("Update"))
            //{
            //    manager.UpdateManager();
            //}
        }
    }
}
