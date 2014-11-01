using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/PlayerDropHealthPotionChanceModifier")]
    public class PlayerDropHealthPotionChanceModifier : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHealthChanged)]
        public void OnObjectHealthChanged(float changedAmount, GameValue.GameValue health)
        {
            float currentChange =
                ChanceBasedEventManager.Instance.EventCurrentChances[(int) Managers.ChanceBasedEvent.DropHealthPotion];

            if (health.Percentage < 0.1f && currentChange < 0.2f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.2f);
            }
            else if (health.Percentage < 0.3f && currentChange < 0.1f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.1f);
            }
            else if (health.Percentage < 0.7f && currentChange < 0.02f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.02f);
            }
        }
    }
}
