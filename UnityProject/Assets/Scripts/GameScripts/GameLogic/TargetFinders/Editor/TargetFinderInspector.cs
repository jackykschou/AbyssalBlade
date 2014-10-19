using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using Assets.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(TargetFinder))]
    public class TargetFinderInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            TargetFinder finder = (TargetFinder)target;

            if (finder.TargetTags == null)
            {
                finder.TargetTags = new List<string>();
            }

            finder.TargetTags.Resize(EditorGUILayout.IntField("Target tag list size", finder.TargetTags.Count));

            for (int i = 0; i < finder.TargetTags.Count; ++i)
            {
                finder.TargetTags[i] = EditorGUILayout.TagField(finder.TargetTags[i]);
            }

            if (finder.TargetPhysicalLayers == null)
            {
                finder.TargetPhysicalLayers = new List<int>();
            }

            finder.TargetPhysicalLayers.Resize(EditorGUILayout.IntField("Target physical layer list size", finder.TargetPhysicalLayers.Count));

            for (int i = 0; i < finder.TargetPhysicalLayers.Count; ++i)
            {
                finder.TargetPhysicalLayers[i] = EditorGUILayout.LayerField(finder.TargetPhysicalLayers[i]);
            }

            if (finder.TargetEffectAppliers == null)
            {
                finder.TargetEffectAppliers = new List<TargetEffectApplier>();
            }

            finder.TargetEffectAppliers.Resize(EditorGUILayout.IntField("Target effect applier list size", finder.TargetEffectAppliers.Count));


            for (int i = 0; i < finder.TargetEffectAppliers.Count; ++i)
            {
#pragma warning disable 618
                finder.TargetEffectAppliers[i] = EditorGUILayout.ObjectField("Applier" + i, finder.TargetEffectAppliers[i], typeof(TargetEffectApplier)) as TargetEffectApplier;
            }

            if (finder.FinderPosition != null)
            {
                finder.FinderPosition.Position = EditorGUILayout.ObjectField("Finder Position", finder.FinderPosition.Position, typeof(Transform)) as Transform;
                finder.FinderPosition.Follower = EditorGUILayout.ObjectField("FinderPosition Follower", finder.FinderPosition.Follower, typeof(Transform)) as Transform;
#pragma warning restore 618
            }
        }
    }
}
