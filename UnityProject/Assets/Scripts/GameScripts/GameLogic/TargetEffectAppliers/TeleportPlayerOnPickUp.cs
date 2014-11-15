using Assets.Scripts.Managers;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/TeleportPlayerOnPickUp")]
    public class TeleportPlayerOnPickUp : TargetEffectApplier
    {
        protected override void ApplyEffect(GameObject target)
        {
            GameEventManager.Instance.TriggerGameEvent(GameEvent.SurvivalSectionEnded);
        }
    }
}
