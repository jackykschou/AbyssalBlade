using Assets.Scripts.Constants;
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
            TriggerGameEvent(GameEvent.OnSectionEnemyDespawned, gameObject, SectionId);
        }

        protected override void Deinitialize()
        {
        }
    }
}
