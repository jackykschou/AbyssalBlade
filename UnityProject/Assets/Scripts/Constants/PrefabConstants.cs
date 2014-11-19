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
        SpawnParticleSystem = 106,
        SurvivalArea1Section2 = 150,
        SurvivalArea1Section3 = 151,
        SurvivalArea2Section2 = 153,
        SurvivalArea2Section3 = 154,
        SurvivalArea3Section2 = 155,
        SurvivalArea3Section3 = 156,
        SurvivalArea4Section2 = 157,
        SurvivalArea4Section3 = 158,
        SurvivalArea5Section2 = 159,
        SurvivalArea5Section3 = 160,


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
        ReflectDamgeParticle = 36,
        AttackUpPowerUp = 37,
        AttackUpPowerUpPickUp = 38,
        HeavyShootChargeParticle = 39,
        OnDeathExplosion = 40,
        ExplosionOnDeathSmoke = 41,
        OnDeathPreExplosion = 42,
        OnDeathExplodePowerUp = 43,
        BerserkPowerUp = 44,
        HealAuraPowerUp = 45,
        SlowAuraPowerUp = 46,
        SimpleLightMeleeEnemyMedium = 47,
        SimpleLightMeleeEnemyHard = 48,
        SimpleRangeEnemyMedium = 49,
        SimpleRangeEnemyHard = 50,
        StoneEnemyMedium = 51,
        StoneEnemyHard = 52,
        SimpleRangeEnemyMediumNoDrop = 53,
        SimpleRangeEnemyHardNoDrop = 54,
        MediumProjectile = 55,
        HardProjectile = 56
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
            {Prefab.SurvivalArea1Section2, "Prefabs/PreloadedPrefab/Level/SurvivalArea1Section2"},
            {Prefab.SurvivalArea2Section2, "Prefabs/PreloadedPrefab/Level/SurvivalArea2Section2"},
            {Prefab.SurvivalArea3Section2, "Prefabs/PreloadedPrefab/Level/SurvivalArea3Section2"},
            {Prefab.SurvivalArea4Section2, "Prefabs/PreloadedPrefab/Level/SurvivalArea4Section2"},
            {Prefab.SurvivalArea5Section2, "Prefabs/PreloadedPrefab/Level/SurvivalArea5Section2"},
            {Prefab.SurvivalArea1Section3, "Prefabs/PreloadedPrefab/Level/SurvivalArea1Section3"},
            {Prefab.SurvivalArea2Section3, "Prefabs/PreloadedPrefab/Level/SurvivalArea2Section3"},
            {Prefab.SurvivalArea3Section3, "Prefabs/PreloadedPrefab/Level/SurvivalArea3Section3"},
            {Prefab.SurvivalArea4Section3, "Prefabs/PreloadedPrefab/Level/SurvivalArea4Section3"},
            {Prefab.SurvivalArea5Section3, "Prefabs/PreloadedPrefab/Level/SurvivalArea5Section3"},
            {Prefab.CritChanceUpPowerUp, "Prefabs/PreloadedPrefab/PowerUp/CritChanceUpPowerUp"},
            {Prefab.CritChanceUpPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/CritChanceUp"},
            {Prefab.DefenseUpPowerUp, "Prefabs/PreloadedPrefab/PowerUp/DefenseUpPowerUp"},
            {Prefab.DefenseUpPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/DefenseUp"},
            {Prefab.ReflectDamgeParticle, "Prefabs/PreloadedPrefab/Particle/ReflectDamgeParticle"},
            {Prefab.SpawnParticleSystem, "Prefabs/PreloadedPrefab/Particle/SpawnParticleSystem"},
            {Prefab.AttackUpPowerUp, "Prefabs/PreloadedPrefab/PowerUp/AttackUpPowerUp"},
            {Prefab.AttackUpPowerUpPickUp, "Prefabs/PreloadedPrefab/PowerUp/AttackUp"},
            {Prefab.HeavyShootChargeParticle, "Prefabs/PreloadedPrefab/Particle/HeavyShootChargeParticle"},
            {Prefab.OnDeathExplosion, "Prefabs/PreloadedPrefab/Particle/EnemyExplosionOnDeath"},
            {Prefab.ExplosionOnDeathSmoke, "Prefabs/PreloadedPrefab/Particle/ExplosionOnDeathSmoke"},
            {Prefab.OnDeathPreExplosion, "Prefabs/PreloadedPrefab/Particle/OnDeathPreExplosion"},
            {Prefab.OnDeathExplodePowerUp, "Prefabs/PreloadedPrefab/PowerUp/ExplodeOnDeathPowerUp"},
            {Prefab.BerserkPowerUp, "Prefabs/PreloadedPrefab/PowerUp/BerserkPowerUp"},
            {Prefab.HealAuraPowerUp, "Prefabs/PreloadedPrefab/PowerUp/HealAuraPowerUp"},
            {Prefab.SlowAuraPowerUp, "Prefabs/PreloadedPrefab/PowerUp/SlowAuraPowerUp"},
            {Prefab.SimpleLightMeleeEnemyMedium, "Prefabs/PreloadedPrefab/Character/SmallMeleeEnemyMedium"},
            {Prefab.SimpleLightMeleeEnemyHard, "Prefabs/PreloadedPrefab/Character/SmallMeleeEnemyHard"},
            {Prefab.SimpleRangeEnemyMedium, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemyMedium"},
            {Prefab.SimpleRangeEnemyHard, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemyHard"},
            {Prefab.StoneEnemyMedium, "Prefabs/PreloadedPrefab/Character/StoneEnemyMedium"},
            {Prefab.StoneEnemyHard, "Prefabs/PreloadedPrefab/Character/StoneEnemyHard"},
            {Prefab.SimpleRangeEnemyMediumNoDrop, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemyMediumNoDrop"},
            {Prefab.SimpleRangeEnemyHardNoDrop, "Prefabs/PreloadedPrefab/Character/SmallRangeEnemyHardNoDrop"},
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
