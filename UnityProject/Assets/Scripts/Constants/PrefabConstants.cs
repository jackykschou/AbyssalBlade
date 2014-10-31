using System;
using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public enum Prefab
    {
        MainMenu = 4,
        TutorialLevel = 3,
        MainLevelArea1 = 10,
        MainLevelArea2 = 12,
        BossLevel = 14,

        Projectile = 0,
        Projectile2 = 5,
        EnemyProjectile = 9,
        BossMissle = 16,
        GreenRangeEnemyProjectile = 18,

        BossMissleExplosion = 15,

        StoneEnemy = 2,
        GreenRangeEnemy = 17,
        Boss1 = 13,
        RegenStoneEnemy = 19,       //!!!
        SimpleLightMeleeEnemy = 7,
        SimpleRangeEnemy = 8,

        HealthPotion = 11,

        DamageText = 1,
        MessageText = 6
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
            {Prefab.BossMissle, "Prefabs/PreloadedPrefab/Projectile/BossMissle"},
            {Prefab.GreenRangeEnemy, "Prefabs/PreloadedPrefab/Character/GreenSmallRangeEnemy"},
            {Prefab.GreenRangeEnemyProjectile, "Prefabs/PreloadedPrefab/Projectile/SmallRangeEnemyProjectile"},
            {Prefab.RegenStoneEnemy, "Prefabs/PreloadedPrefab/Character/RegenStoneEnemy"}
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
