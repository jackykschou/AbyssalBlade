using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    public class HealOnKill : PowerUp
    {
        public HealthChanger HealthChanger;

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectKills)]
        public void OnObjectKills(GameValue.GameValue gameValue, GameValueChanger gameValueChanger, float amount, bool crited)
        {
            
        }

        protected override void Apply()
        {

        }

        protected override void UnApply()
        {

        }
    }
}
