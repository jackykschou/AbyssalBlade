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
        public Transform Fill;
        public Transform Icon;

        private Image fillImage;
        private Image iconImage;

        protected override void Initialize()
        {
            base.Initialize();
            fillImage = Fill.GetComponent<Image>();
            CooldownBar = GetComponent<Slider>();
            iconImage = Icon.GetComponent<Image>();
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

        [GameEventAttribute(GameEvent.DisableAbility)]
        public void DisableAbility(int id)
        {
            if (id == SkillId)
            {
                if (fillImage != null)
                    fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 0.0f);
                if (iconImage != null)
                    iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0.0f);
            }
        }

        [GameEventAttribute(GameEvent.EnableAbility)]
        public void EnableAbility(int id)
        {
            if (id == SkillId)
            {
                if (fillImage != null)
                    fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 1.0f);
                if (iconImage != null)
                    iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0.5f);
            }
        }
    }
}
