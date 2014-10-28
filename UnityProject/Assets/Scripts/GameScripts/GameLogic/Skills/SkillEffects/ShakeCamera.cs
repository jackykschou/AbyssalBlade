using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class ShakeCamera : SkillEffect
    {
        [Range(0f, 1f)] public float ShakeIntensity;

        public override void Activate()
        {
            base.Activate();
            TriggerGameEvent(GameEvent.ShakeCamera, ShakeIntensity);
            Activated = false;
        }
    }
}
