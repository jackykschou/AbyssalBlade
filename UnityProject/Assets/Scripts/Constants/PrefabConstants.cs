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
        SurvivalLevel = 100,
        SurvivalArea1 = 101,
        SurvivalArea2 = 102,
        SurvivalArea3 = 103,
        SurvivalArea4 = 104,
        SurvivalArea5 = 105,

        Projectile = 0,
        Projectile2 = 5,
        EnemyProjectile = 9,
        BossMissle = 16,
        GreenRangeEnemyProjectile = 18,

        BossMissleExplosion = 15,

        StoneEnemy = 2,
        GreenRangeEnemy = 17,
        Boss1 = 13,
        RegenStoneEnemy = 19,
        SimpleLightMeleeEnemy = 7,
        SimpleRangeEnemy = 8,
        SimpleRangeNoDropEnemy = 23,

        HealthPotion = 11,

        DamageText = 1,
        MessageText = 6,

        IntroStoryBoardLevel = 22,

        ShotGunSmoke = 24,
        ShotGunTrail = 25,
        None = 26, 
        HealOnKillTrail = 27,
        HealOnKillPowerUp = 28,
        HealOnKillPowerUpPickUp = 29,
        DamageReflectPowerUp = 30,
        DamageReflectPowerUpPickUp = 31,
        CritChanceUpPowerUp = 32,
        CritChanceUpPowerUpPickUp = 33,
        DefenseUpPowerUp = 34,
        DefenseUpPowerUpPickUp = 35,
        ReflectDamgeParticle = 36
    };

    public class PrefabConstants
    {
        public const string PrefabExtension = ".prefab";
        public const string StartingAssetPrefabPath = "Assets/Resources/Prefabs/PreloadedPrefab/";
        public const string StartingResourcesPrefabPath = "Prefabs/PreloadedPrefab/";

        private static readonly Dictionary<Prefab, string> PrefabPathMap = new Dictionary<Prefab, string>()
        {
            {Prefab.Projectile, "Prefabs/PreloadedPrefab/Projectile/Projectile"},
            {Prefab.DamageText, "Prefabs/PreloadedPrefab/TextMeshes/DamageTextMesh"},
            {Prefab.StoneEnemy, "Prefabs/PreloadedPrefab/Character/StoneEnemy"},
            {Prefab.TutorialLevel, "Prefabs/PreloadedPrefab/Level/TutorialLevel"},
            {Prefab.MainMenu, "Prefabs/PreloadedPrefab/Menu/MainMenu"},
            {Prefab.Projectile2, "Prefabs/PreloadedPrefab/Projectile/Projectile2"},
            {Prefab.MessageText, "Prefabs/PreloadedPrefab/TextMeshes/MessageTextMesh"},
            {Prefab.SimpleLightMeleeEnemy, "Prefabs/PreloadedPrefab/Character/SmallMeleeEnemy"},
            {Prefab.SimpleRangeEnemy, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemy"},
            {Prefab.EnemyProjectile, "Prefabs/PreloadedPrefab/Projectile/EnemyProjectile"},
            {Prefab.MainLevelArea1, "Prefabs/PreloadedPrefab/Level/MainLevelArea1"},
            {Prefab.HealthPotion, "Prefabs/PreloadedPrefab/PowerUp/HealthPotion"},
            {Prefab.MainLevelArea2, "Prefabs/PreloadedPrefab/Level/MainLevelArea2"},
            {Prefab.Boss1, "Prefabs/PreloadedPrefab/Character/BossEnemy"},
            {Prefab.BossLevel, "Prefabs/PreloadedPrefab/Level/BossLevel"},
            {Prefab.BossMissleExplosion, "Prefabs/PreloadedPrefab/Projectile/BossMissleExplosion"},
            {Prefab.BossMissle, "Prefabs/PreloadedPrefab/Projectile/BossMissle"},
            {Prefab.GreenRangeEnemy, "Prefabs/PreloadedPrefab/Character/GreenSmallRangeEnemy"},
            {Prefab.GreenRangeEnemyProjectile, "Prefabs/PreloadedPrefab/Projectile/SmallRangeEnemyProjectile"},
            {Prefab.RegenStoneEnemy, "Prefabs/PreloadedPrefab/Character/RegenStoneEnemy"},
            {Prefab.IntroStoryBoardLevel, "Prefabs/PreloadedPrefab/Level/StoryBoardLevel"},
            {Prefab.SimpleRangeNoDropEnemy, "Prefabs/PreloadedPrefab/Character/SimpleRangeNoDropEnemy"},
            {Prefab.ShotGunSmoke, "Prefabs/PreloadedPrefab/Particle/ShotGunSmoke"},
            {Prefab.SurvivalLevel, "Prefabs/PreloadedPrefab/Level/SurvivalLevel"},
            {Prefab.ShotGunTrail, "Prefabs/PreloadedPrefab/Particle/ShotGunTrail"},
            {Prefab.HealOnKillTrail, "Prefabs/PreloadedPrefab/Particle/HealOnKillTrail"},
            {Prefab.HealOnKillPowerUp, "Prefabs/PreloadedPrefab/PowerUp/HealOnKillPowerUp"},
            {Prefab.HealOnKillPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/HealOnKill"},
            {Prefab.DamageReflectPowerUp, "Prefabs/PreloadedPrefab/PowerUp/ReflectDamagePowerUp"},
            {Prefab.DamageReflectPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/ReflectDamage"},
            {Prefab.SurvivalArea1, "Prefabs/PreloadedPrefab/Level/SurvivalArea1"},
            {Prefab.SurvivalArea2, "Prefabs/PreloadedPrefab/Level/SurvivalArea2"},
            {Prefab.SurvivalArea3, "Prefabs/PreloadedPrefab/Level/SurvivalArea3"},
            {Prefab.SurvivalArea4, "Prefabs/PreloadedPrefab/Level/SurvivalArea4"},
            {Prefab.SurvivalArea5, "Prefabs/PreloadedPrefab/Level/SurvivalArea5"},
            {Prefab.CritChanceUpPowerUp, "Prefabs/PreloadedPrefab/PowerUp/CritChanceUpPowerUp"},
            {Prefab.CritChanceUpPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/CritChanceUp"},
            {Prefab.DefenseUpPowerUp, "Prefabs/PreloadedPrefab/PowerUp/DefenseUpPowerUp"},
            {Prefab.DefenseUpPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/DefenseUp"},
            {Prefab.ReflectDamgeParticle, "Prefabs/PreloadedPrefab/Particle/ReflectDamgeParticle"},
            {Prefab.None, ""}
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
