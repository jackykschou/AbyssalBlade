using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Slider))]
    public class SkillCoolDownSlider : GameLogic
    {
        [Range(0, 5)] // 0 is movement skill
        public int SkillId;
        public Slider CooldownBar;
        public Transform Fill;
        public Transform Icon;
        public Transform Highlight;

        private Image _buttonIconImage;
        private Image _buttonIconImageHighlight;

        protected override void Initialize()
        {
            base.Initialize();
            CooldownBar = GetComponent<Slider>();
            if(Icon)
                _buttonIconImage = Icon.GetComponent<Image>();
            if(Highlight)
                _buttonIconImageHighlight = Highlight.GetComponent<Image>();
        }

        protected override void Deinitialize()
        {
            DisableHighlight(SkillId);
        }

        [GameEventAttribute(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                CooldownBar.value = percentage;
            }
        }

        [GameEventAttribute(GameEvent.EnableHighlightSkill)]
        public void EnableHighlight(int id, float origHighlightDuration)
        {
            if (id == SkillId)
            {
                if (_buttonIconImageHighlight != null && _buttonIconImage != null)
                {

                    _buttonIconImageHighlight.color = new Color(_buttonIconImageHighlight.color.r,
                                                                _buttonIconImageHighlight.color.g,
                                                                _buttonIconImageHighlight.color.b, 1.0f);
                    _buttonIconImage.color = new Color(_buttonIconImage.color.r,
                                                       _buttonIconImage.color.g,
                                                       _buttonIconImage.color.b, 1.0f);
                }
            }
        }

        [GameEventAttribute(GameEvent.DisableHighlightSkill)]
        public void DisableHighlight(int id)
        {
            if (id == SkillId)
            {
                if (_buttonIconImageHighlight != null && _buttonIconImage != null)
                {
                    _buttonIconImageHighlight.color = new Color(_buttonIconImageHighlight.color.r, 
                                                                _buttonIconImageHighlight.color.g,
                                                                _buttonIconImageHighlight.color.b, 0.0f);
                    _buttonIconImage.color = new Color(_buttonIconImage.color.r, 
                                                       _buttonIconImage.color.g, 
                                                       _buttonIconImage.color.b, 0.0f);
                }
            }
        }
    }
}
