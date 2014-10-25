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
            _slider = gameObject.GetComponent<Slider>();
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            _slider.value = HealthToWatch.HitPoint.Value / HealthToWatch.HitPoint.Max;
            if (HealthSliderImage != null)
                HealthSliderImage.color = Color.Lerp(Color.red, Color.green, _slider.value);
        }
    }
}
