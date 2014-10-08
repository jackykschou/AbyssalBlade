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
        public Transform Highlight;

        private Image fillImage;
        private Image iconImage;
        private Image highlightImage;
        private bool highlight;
        private float highlightDuration;
        private float origHighlightDuration;
        private int highlightedSkill;
        private bool ON;

        protected override void Initialize()
        {
            base.Initialize();
            fillImage = Fill.GetComponent<Image>();
            CooldownBar = GetComponent<Slider>();
            iconImage = Icon.GetComponent<Image>();
            highlightImage = Highlight.GetComponent<Image>();
            highlight = false;
            origHighlightDuration = 0.0f;
            highlightedSkill = -1;
            ON = false;
        }

        protected override void Deinitialize()
        {
            highlight = false;
            highlightDuration = 0.0f;
            highlightedSkill = -1;
            highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 0.0f);
        }

        protected override void Update()
        {
            base.Update();
            if (SkillId == highlightedSkill)
            {
                if (highlight)
                {
                    if (highlightDuration < 0.0f)
                    {
                        ON = !ON;
                        highlightDuration = origHighlightDuration;
                    }

                    if(ON)
                        highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 0.0f);
                    else
                        highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 1.0f);

                    highlightDuration -= Time.deltaTime;
                }
            }
        }

        [GameEventAttribute(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                CooldownBar.value = percentage;
            }
        }

        [GameEventAttribute(GameEvent.DisableAbilityUI)]
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

        [GameEventAttribute(GameEvent.EnableAbilityUI)]
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

        [GameEventAttribute(GameEvent.EnableHighlightSkill)]
        public void EnableHighlight(int id, float origHighlightDuration)
        {
            highlight = true;
            highlightedSkill = id;
            highlightDuration = origHighlightDuration;
            this.origHighlightDuration = origHighlightDuration;
        }

        [GameEventAttribute(GameEvent.DisableHighlightSkill)]
        public void DisableHighlight(int id)
        {
            highlight = false;
            highlightDuration = 0.0f;
            highlightedSkill = -1;
            highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 0.0f);
        }
    }
}
