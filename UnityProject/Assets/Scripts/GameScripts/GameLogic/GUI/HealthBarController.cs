using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class HealthBarController : GameLogic
    {
        public Slider healthBarSlider;
        public Text healthText;
        GameValue hitPoints;

        private float _startingHealth;
        private float _damageTaken;
        private float _damageHealed;

        protected override void Initialize()
        {
 	        base.Initialize();
            hitPoints = this.gameObject.GetComponent<Destroyable.Destroyable>().HitPoint;
            if(hitPoints == null)
            {
                Debug.Log("Could not find hit points");
                return;
            }
            healthText.text = "100 %";
            healthBarSlider.value = 100;
            _startingHealth = (int)hitPoints.InitialValue;
            _damageTaken = 0.0f;
            _damageHealed = 0.0f;
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage)
        {
            if(healthBarSlider == null)
            {
                Debug.Log("HealthBar Controller: Slider is NULL.");
                return;
            }
            if (healthText == null)
            {
                Debug.Log("HealthBar Controller: Text is NULL.");
                return;
            }
            if (healthBarSlider.value == 0)
                return;
            _damageTaken += (int)damage;
            healthBarSlider.value = ((_startingHealth - _damageTaken) / _startingHealth) * 100;
            healthText.text = ((int)healthBarSlider.value).ToString() + " %";
        }

        protected override void Deinitialize()
        {
        }
    }
}
