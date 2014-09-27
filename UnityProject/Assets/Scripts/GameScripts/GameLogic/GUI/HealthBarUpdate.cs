using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Destroyable.Destroyable))]
    public class HealthBarUpdate : GameLogic
    {
        Destroyable.Destroyable _HitPointDestroyer;
        float _initialHealth;

        protected override void Initialize()
        {
            base.Initialize();
            _HitPointDestroyer = gameObject.GetComponent<Destroyable.Destroyable>();
            _initialHealth = _HitPointDestroyer.HitPoint.InitialValue;
        }

        protected override void Update()
        {
            base.Update();
            TriggerGameEvent(GameEvent.PlayerHealthUpdate, _HitPointDestroyer.HitPoint.Value/_initialHealth);
        }

        protected override void Deinitialize()
        {
        }
    }
}