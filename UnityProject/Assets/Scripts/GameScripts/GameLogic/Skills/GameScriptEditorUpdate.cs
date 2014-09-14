using System.Linq;
using System.Reflection;
using Assets.Scripts.GameScripts.Components;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [ExecuteInEditMode]
    public sealed class GameScriptEditorUpdate : MonoBehaviour 
    {
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
    }
}
