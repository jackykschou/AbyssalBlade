using System.Linq;
using System.Reflection;
using Assets.Scripts.GameScripts.Components;
using UnityEngine;

namespace Assets.Scripts.GameScripts
{
    [ExecuteInEditMode]
    public sealed class GameScriptEditorUpdate : MonoBehaviour 
    {
#if UNITY_EDITOR
        void Update()
        {
            GetComponents<GameScript>().ToList().
                ForEach(s =>
                {
                    s.EditorUpdate();
                    s.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Select(f => f.GetValue(s) as GameScriptComponent)
                            .Where(c => c != null).ToList().ForEach(c => c.EditorUpdate());
                });
        }
#endif
    }
}
