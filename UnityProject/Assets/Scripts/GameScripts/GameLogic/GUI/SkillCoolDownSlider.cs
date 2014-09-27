using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Slider))]
    public class SkillCoolDownSlider : GameLogic
    {
        [Range(0, 4)] // 0 is movement skill
        public int SkillId;
        public Slider CooldownBar;

        protected override void Initialize()
        {
            base.Initialize();
            CooldownBar = GetComponent<Slider>();
        }

        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                CooldownBar.value = percentage;
            }
        }
    }
}
