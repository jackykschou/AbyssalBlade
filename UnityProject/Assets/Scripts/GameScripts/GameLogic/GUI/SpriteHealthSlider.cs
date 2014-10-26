using Assets.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class SpriteHealthSlider : GameLogic
    {
        public Health.Health HealthToWatch;
        public Image HealthSliderImage;
        private Slider _slider;

        protected override void Initialize()
        {
            base.Initialize();
            _slider = gameObject.GetComponentInChildren<Slider>();
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void UpdateSlider(float damage, bool crit)
        {
            _slider.value = HealthToWatch.HitPoint.Percentage;
            if (HealthSliderImage != null)
                HealthSliderImage.color = Color.Lerp(Color.red, Color.green, _slider.value);
        }
    }
}
