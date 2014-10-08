﻿using System.Collections;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class CharacterInterrupt : GameLogic
    {
        public bool Interrupted { get; private set; }

        [Range(0f, float.MaxValue)]
        public float HitDamageThreshold;
        [Range(0f, float.MaxValue)]
        public float InterruptionDuration;

        public FixTimeDispatcher HitDamageThresholdResetTime;
        public FixTimeDispatcher InterruptCoolDown;

        [GameScriptEventAttribute(GameScriptEvent.InterruptCharacter)]
        public void InterruptCharacter()
        {
            if (!InterruptCoolDown.CanDispatch())
            {
                return;
            }
            InterruptCoolDown.Dispatch();
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterInterrupted);
            StartCoroutine(CountDownInterruption());
        }

        IEnumerator CountDownInterruption()
        {
            Interrupted = true;
            yield return new WaitForSeconds(InterruptionDuration);
            Interrupted = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Interrupted = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
