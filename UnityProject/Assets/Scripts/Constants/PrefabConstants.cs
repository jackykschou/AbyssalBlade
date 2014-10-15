using System;
using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public enum Prefab
    {
        Projectile,
        DamageText,
        StoneEnemy,
        TutorialLevel,
        MainMenu,
        Projectile2,
        MessageText,
        SimpleLightMeleeEnemy,
        SimpleRangeEnemy,
        EnemyProjectile
    };

    public class PrefabConstants
    {
        public const string PrefabExtension = ".prefab";
        public const string StartingAssetPrefabPath = "Assets/Resources/Prefabs/";
        public const string StartingResourcesPrefabPath = "Prefabs/";

        private static readonly Dictionary<Prefab, string> PrefabPathMap = new Dictionary<Prefab, string>()
        {
            {Prefab.Projectile, "Prefabs/PreloadedPrefab/Projectile/Projectile"},
            {Prefab.DamageText, "Prefabs/PreloadedPrefab/TextMeshes/DamageTextMesh"},
            {Prefab.StoneEnemy, "Prefabs/PreloadedPrefab/Character/StoneEnemy"},
            {Prefab.TutorialLevel, "Prefabs/TutorialLevel"},
            {Prefab.MainMenu, "Prefabs/Menu/MainMenu"},
            {Prefab.Projectile2, "Prefabs/PreloadedPrefab/Projectile/Projectile2"},
            {Prefab.MessageText, "Prefabs/PreloadedPrefab/TextMeshes/MessageTextMesh"},
            {Prefab.SimpleLightMeleeEnemy, "Prefabs/PreloadedPrefab/Character/SmallMeleeEnemy"},
            {Prefab.SimpleRangeEnemy, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemy"},
            {Prefab.EnemyProjectile, "Prefabs/PreloadedPrefab/Projectile/EnemyProjectile"}
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
