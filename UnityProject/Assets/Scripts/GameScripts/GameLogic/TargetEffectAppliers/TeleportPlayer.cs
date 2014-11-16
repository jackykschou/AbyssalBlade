using Assets.Scripts.Managers;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/TeleportPlayer")]
    public class TeleportPlayer : TargetEffectApplier
    {
        private ParticleSystem _playerParticles;
        protected override void Initialize()
        {
            base.Initialize();
            _playerParticles = GameObject.Find("SurvivalTeleportParticles").GetComponent<ParticleSystem>();
        }

        protected override void ApplyEffect(GameObject target)
        {
            GameEventManager.Instance.TriggerGameEvent(GameEvent.SurvivalSectionEnded);
            if(_playerParticles != null)
                _playerParticles.Play();
        }
    }
}
