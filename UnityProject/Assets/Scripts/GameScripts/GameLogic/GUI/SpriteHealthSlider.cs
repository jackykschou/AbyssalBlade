using Assets.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class SpriteHealthSlider : GameLogic
    {
        public Image HealthSliderImage;
        private Slider _slider;

        protected override void Initialize()
        {
            base.Initialize();
            _slider = gameObject.GetComponentInChildren<Slider>();
            _slider.targetGraphic.enabled = true;
            _slider.value = 1.0f;
            HealthSliderImage.color = Color.green;
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeHeal)]
        public void UpdateSlider(float damage, bool crit, GameValue.GameValue health)
        {
            _slider.value = health.Percentage;
            if (HealthSliderImage != null)
            {
                HealthSliderImage.color = Color.Lerp(Color.red, Color.green, _slider.value);
                if (Mathf.Approximately(health.Percentage, 0f))
                {
                    _slider.targetGraphic.enabled = false;
                }
                else
                {
                    _slider.targetGraphic.enabled = true;
                }
            }
        }
    }
}
