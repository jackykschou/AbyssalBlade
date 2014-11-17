﻿using Assets.Scripts.Constants;
using Assets.Scripts.Utility;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    public class DamageUp : PowerUp
    {
        public float ChangeAmount;

        protected override void Deinitialize()
        {
        }

        protected override void Apply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeHealthChangerDamageRawAmountToInitialPercentage, 1.0f + (ChangeAmount * AppliedCounter));
        }

        protected override void UnApply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeHealthChangerDamageRawAmountToInitialPercentage, 1.0f);
        }
    }
}
