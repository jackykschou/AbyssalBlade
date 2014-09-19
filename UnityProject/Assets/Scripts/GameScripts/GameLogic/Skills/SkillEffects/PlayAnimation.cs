using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Animator;
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
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorState, BoolParameterName);
            StartCoroutine(WaitForAnimationFinish());
        }

        IEnumerator WaitForAnimationFinish()
        {
            List<bool> messagesSent = new List<bool>(_animationEventMessages.Count);
            for (int i = 0; i < _animationEventMessages.Count; ++i)
            {
                messagesSent.Add(false);
            }
            UnityEngine.Animator casterAnimator = SKill.Caster.gameObject.GetComponent<ObjectAnimator>().Animator;
            float timer = _animationDuration;
            while (timer >= 0)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                timer -= Time.deltaTime;
                for (int i = 0; i < _animationEventMessages.Count; ++i)
                {
                    if (!messagesSent[i] &&((casterAnimator.GetCurrentAnimatorStateInfo(0).length >= _animationEventMessagesSendTime[i]) ||
                        (timer >= 0)))
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
