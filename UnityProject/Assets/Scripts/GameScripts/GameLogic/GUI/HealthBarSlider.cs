using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBarSlider : GameLogic
    {
        private Slider _HealthBar;
        private Text _HealthText; // make required?
        private Image _HealthColorImage;

        protected override void Initialize()
        {
            base.Initialize();
            _HealthBar = GetComponent<Slider>();
            _HealthText = GetComponentInChildren<Text>();
            _HealthColorImage = GameObject.Find("HealthBarFill").GetComponent<Image>();
            _HealthBar.value = 1.0f;
        }

        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.PlayerHealthUpdate)]
        public void UpdateHealth(float percentage)
        {
            _HealthBar.value = percentage;
            int PercentageInt = (int)(percentage * 100);
            _HealthText.text = PercentageInt.ToString() + "%";

            if (_HealthColorImage != null)
                _HealthColorImage.color = Color.Lerp(Color.red, Color.green, _HealthBar.value);
        }
    }
}
