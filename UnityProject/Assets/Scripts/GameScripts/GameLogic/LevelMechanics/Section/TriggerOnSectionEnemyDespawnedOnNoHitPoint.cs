using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/TriggerOnSectionEnemyDespawnedOnNoHitPoint")]
    public class TriggerOnSectionEnemyDespawnedOnNoHitPoint : SectionLogic 
    {
        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DecrementSectionEnemy()
        {
            TriggerGameEvent(GameEvent.OnSectionEnemyDespawned, SectionId);
        }

        protected override void Initialize()
        {
            base.Initialize();
            SectionId = LevelManager.Instance.CurrentSectionId;
        }

        protected override void Deinitialize()
        {
        }
    }
}
