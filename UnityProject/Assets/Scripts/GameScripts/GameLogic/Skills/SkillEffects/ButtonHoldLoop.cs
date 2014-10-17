using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ButtonHoldLoop")]
    public class ButtonHoldLoop : SkillEffect
    {
        [SerializeField] private ButtonOnHold _button;

        [SerializeField] private List<string> _onHoldFinishEventMessages;

        [SerializeField]
        private List<string> _loopEventMessages;

        [SerializeField]
        private List<float> _loopEventMessagesSendTime;

        [Range(0f, 10)] 
        public float TimePerLoop;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_loopEventMessages == null)
            {
                return;
            }

            if (_loopEventMessages.Count != _loopEventMessagesSendTime.Count)
            {
                _loopEventMessagesSendTime.Resize(_loopEventMessages.Count);
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(StartHolding());
        }

        private IEnumerator StartHolding()
        {
            float holdTimer = 0f;
            float loopTimer = 0f;
            List<bool> messagesSent = new List<bool>();
            messagesSent.AddRange(Enumerable.Repeat(false, _loopEventMessages.Count));
            TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, 0f);

            while (_button.Detect() && !Skill.Caster.gameObject.IsInterrupted())
            {
                for (int i = 0; i < _loopEventMessages.Count; ++i)
                {
                    if (!messagesSent[i] && (loopTimer >= _loopEventMessagesSendTime[i]))
                    {
                        messagesSent[i] = true;
                        gameObject.BroadcastMessage(_loopEventMessages[i], SendMessageOptions.DontRequireReceiver);
                    }
                }

                loopTimer += Time.deltaTime;
                holdTimer += Time.deltaTime;

                if (loopTimer >= TimePerLoop)
                {
                    loopTimer = 0f;
                    messagesSent.ForEach(b => b = false);
                }
                yield return new WaitForSeconds(Time.deltaTime);
                TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, holdTimer);
            }
            _onHoldFinishEventMessages.ForEach(s => gameObject.BroadcastMessage(s, SendMessageOptions.DontRequireReceiver));
            TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, _button.LastHoldTime);
            Activated = false;
        }
    }
}
