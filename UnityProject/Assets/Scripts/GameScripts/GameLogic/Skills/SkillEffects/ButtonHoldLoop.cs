using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ButtonHoldLoop")]
    public class ButtonHoldLoop : SkillEffect
    {
        [SerializeField] private ButtonOnHold _button;

        [SerializeField] private List<string> _onHoldFinishEventMessages;

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(StartHolding());
        }

        private IEnumerator StartHolding()
        {
            while (_button.Detect())
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _onHoldFinishEventMessages.ForEach(s => gameObject.BroadcastMessage(s, SendMessageOptions.DontRequireReceiver));
            TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, _button.LastHoldTime);
            Activated = false;
        }
    }
}
