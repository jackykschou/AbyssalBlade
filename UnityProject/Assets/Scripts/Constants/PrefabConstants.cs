using System;
using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public enum Prefab
    {
        Projectile = 0,
        DamageText = 1,
        StoneEnemy = 2,
        TutorialLevel = 3,
        MainMenu = 4,
        Projectile2 = 5,
        MessageText = 6,
        SimpleLightMeleeEnemy = 7,
        SimpleRangeEnemy = 8,
        EnemyProjectile = 9,
        MainLevelArea1 = 10,
        HealthPotion = 11,
        MainLevelArea2 = 12,
        Boss1 = 13,
        BossLevel = 14,
        BossMissleExplosion = 15,
        BossMissle = 16
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
            {Prefab.EnemyProjectile, "Prefabs/PreloadedPrefab/Projectile/EnemyProjectile"},
            {Prefab.MainLevelArea1, "Prefabs/MainLevelArea1"},
            {Prefab.HealthPotion, "Prefabs/PreloadedPrefab/PowerUp/HealthPotion"},
            {Prefab.MainLevelArea2, "Prefabs/MainLevelArea2"},
            {Prefab.Boss1, "Prefabs/Character/BossEnemy"},
            {Prefab.BossLevel, "Prefabs/BossLevel"},
            {Prefab.BossMissleExplosion, "Prefabs/PreloadedPrefab/Projectile/BossMissleExplosion"},
            {Prefab.BossMissle, "Prefabs/PreloadedPrefab/Projectile/BossMissle"}
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
