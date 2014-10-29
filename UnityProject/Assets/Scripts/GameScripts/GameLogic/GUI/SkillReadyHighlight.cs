using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Image))]
    public class SkillReadyHighlight : GameLogic
    {
        [Range(0,5)]
        public int SkillId;
        [Range(0.0f, 1.0f)]
        public float HighlightDuration = .5f;
        [Range(0.0f, 1.0f)]
        public float MaxAlpha = .75f;

        private bool _highlight;
        private float _highlightDuration;
        private float _origHighlightDuration;
        private bool _on;

        private Image _highlightImage;

        protected override void Update()
        {
            base.Update();
            if (_highlight)
            {
                if (_highlightDuration < 0.0f)
                {
                    _on = !_on;
                    _highlightDuration = _origHighlightDuration;
                }
                if (_highlightImage != null)
                {
                    if (_on)
                    {
                        _highlightImage.color = new Color(_highlightImage.color.r, _highlightImage.color.g, _highlightImage.color.b, 0.0f);
                    }
                    else
                    {
                        _highlightImage.color = new Color(_highlightImage.color.r, _highlightImage.color.g, _highlightImage.color.b, MaxAlpha);
                    }
                }
                _highlightDuration -= Time.deltaTime;
            }
        }

        [GameEventAttribute(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                if (percentage > .99f && !_highlight)
                {
                    EnableHighlight(HighlightDuration);
                }
                else if(percentage < .99f && _highlight)
                {
                    DisableHighlight();
                }
            }
        }
        public void EnableHighlight(float origHighlightDuration)
        {
            _highlightDuration = origHighlightDuration;
            _origHighlightDuration = origHighlightDuration;
            _highlight = true;
        }

        public void DisableHighlight()
        {
            _highlightDuration = 0.0f;
            if (_highlightImage != null)
            {
                _highlightImage.color = new Color(_highlightImage.color.r, _highlightImage.color.g, _highlightImage.color.b, 0.0f);
            }
            _highlight = false;
        }
        protected override void Initialize()
        {
            base.Initialize();
            _highlightImage = GetComponent<Image>();
            _highlight = false;
            _origHighlightDuration = 0.0f;
            _on = false;
        }
        
        protected override void Deinitialize()
        {
        }
    }
}
