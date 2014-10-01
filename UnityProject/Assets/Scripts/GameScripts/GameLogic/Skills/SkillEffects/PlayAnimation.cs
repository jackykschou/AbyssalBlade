using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlayAnimation")]
    public class PlayAnimation : SkillEffect
    {
        public string BoolParameterName;

        [SerializeField] 
        private float _animationDuration;

        [SerializeField] 
        List<string> _animationEventMessages;

        [SerializeField]
        List<float> _animationEventMessagesSendTime;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_animationEventMessages.Count != _animationEventMessagesSendTime.Count)
            {
                _animationEventMessagesSendTime.Resize(_animationEventMessages.Count);
            }
        }

        public override void Activate()
        {
            base.Activate();
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorBoolState, BoolParameterName);
            StartCoroutine(WaitForAnimationFinish());
        }

        IEnumerator WaitForAnimationFinish()
        {
            List<bool> messagesSent = new List<bool>(_animationEventMessages.Count);
            for (int i = 0; i < _animationEventMessages.Count; ++i)
            {
                messagesSent.Add(false);
            }
            float timer = 0f;
            while ((timer < _animationDuration || messagesSent.Any(b => !b)) && !Skill.Caster.gameObject.IsInterrupted())
            {
                yield return new WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                for (int i = 0; i < _animationEventMessages.Count; ++i)
                {
                    if (!messagesSent[i] && (timer >= _animationEventMessagesSendTime[i]))
                    {
                        messagesSent[i] = true;
                        gameObject.BroadcastMessage(_animationEventMessages[i], SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            Activated = false;
        }

    }
}
