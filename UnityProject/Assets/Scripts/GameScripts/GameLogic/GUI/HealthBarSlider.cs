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
        public Text _HealthText; // make required?
        private Image _HealthColorImage;

        protected override void Initialize()
        {
            base.Initialize();
            _HealthBar = GetComponent<Slider>();
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
            int percentageInt = (int)(Mathf.Ceil(percentage * 100f));
            if (_HealthText != null)
            {
                _HealthText.text = percentageInt + "%";
            }

            if (_HealthColorImage != null)
            {
                _HealthColorImage.color = Color.Lerp(Color.red, Color.green, _HealthBar.value);
            }
        }
    }
}
