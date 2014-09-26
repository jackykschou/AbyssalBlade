using System;
using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public enum Prefab
    {
        Projectile,
        DamageText,
        StoneEnemy
    };

    public class PrefabConstants
    {
        public const string PrefabExtension = ".prefab";
        public const string StartingAssetPrefabPath = "Assets/Resources/Prefabs/";
        public const string StartingResourcesPrefabPath = "Prefabs/";

        private static readonly Dictionary<Prefab, string> PrefabPathMap = new Dictionary<Prefab, string>()
        {
            {Prefab.Projectile, "Prefabs/Skills/Projectile"},
            {Prefab.DamageText, "Prefabs/DamageText/DamageTextMesh"},
            {Prefab.StoneEnemy, "Prefabs/Character/StoneEnemy"}
        };

        public static string GetPrefabName(Prefab prefab)
        {
            if (!PrefabPathMap.ContainsKey(prefab))
            {
                throw new Exception("Prefab is not defined");
            }

            return PrefabPathMap[prefab];
        }
    }
}
