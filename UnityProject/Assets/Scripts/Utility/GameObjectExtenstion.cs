using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using Assets.Scripts.GameScripts.GameLogic.Health;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class GameObjectExtenstion
    {
        private static readonly Dictionary<GameObject, GameScript> GameScriptsCache = new Dictionary<GameObject, GameScript>();
        private static readonly Dictionary<GameObject, Health> HealthCache = new Dictionary<GameObject, Health>();
        private static readonly Dictionary<GameObject, CharacterInterrupt> OnHitInterruptCache = new Dictionary<GameObject, CharacterInterrupt>();

        public static void CacheGameObject(this GameObject o)
        {
            GameScript gameScript = o.GetComponent<GameScript>();
            Health health = o.GetComponent<Health>();
            CharacterInterrupt characterInterrupt = o.GetComponent<CharacterInterrupt>();

            if (gameScript != null && !GameScriptsCache.ContainsKey(o))
            {
                GameScriptsCache.Add(o, gameScript);
            }
            if (health != null && !HealthCache.ContainsKey(o))
            {
                HealthCache.Add(o, health);
            }
            if (characterInterrupt != null && !OnHitInterruptCache.ContainsKey(o))
            {
                OnHitInterruptCache.Add(o, characterInterrupt);
            }
        }

        public static void UncacheGameObject(this GameObject o)
        {
            GameScriptsCache.Remove(o);
            HealthCache.Remove(o);
            OnHitInterruptCache.Remove(o);
        }

        public static void TriggerGameScriptEvent(this GameObject o, GameScriptEvent gameScriptEvent, params object[] args)
        {
            if (!GameScriptsCache.ContainsKey(o))
            {
                GameScript gameScript = o.GetComponent<GameScript>();
                if (gameScript != null)
                {
                    gameScript.TriggerGameScriptEvent(gameScriptEvent, args);
                }
                return;
            }

            GameScriptsCache[o].TriggerGameScriptEvent(gameScriptEvent, args);
        }

        public static bool IsInterrupted(this GameObject o)
        {
            if (!OnHitInterruptCache.ContainsKey(o))
            {
                CharacterInterrupt characterInterrupt = o.GetComponent<CharacterInterrupt>();
                if (characterInterrupt != null)
                {
                    return characterInterrupt.Interrupted;
                }
                return false;
            }

            return OnHitInterruptCache[o].Interrupted;
        }

        public static bool HitPointAtZero(this GameObject o)
        {
            if (!HealthCache.ContainsKey(o))
            {
                Health health = o.GetComponent<Health>();
                if (health != null)
                {
                    return health.HitPointAtZero;
                }
                return false;
            }

            return HealthCache[o].HitPointAtZero;
        }
    }
}
