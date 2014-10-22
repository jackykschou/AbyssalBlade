using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValue
{
    [AddComponentMenu("Misc/GameValueChanger")]
    public class GameValueChanger : GameLogic
    {
        public enum NonStackableType
        {
            Haha
        }

        public enum ChangeTargetValueType
        {
            CurrentValue,
            Max,
            Min
        }

        public enum OneTimeChangeDurationType
        {
            Permanent,
            TempFixedTime,
            Nondeterministic
        }

        public enum ByIntervalChangeDurationType
        {
            FixedTime,
            Nondeterministic
        }

        public enum CurrentValueChangeType
        {
            FixedAmount,
            CurrentPercentage,
            MaxPercentage,
            FixedAmountByInterval,
            CurrentPercentageByInterval,
            MaxPercentageByInterval
        }

        public ChangeTargetValueType TargetValueType;
        public OneTimeChangeDurationType OneTimeDurationType;
        public ByIntervalChangeDurationType IntervalDurationType;
        public CurrentValueChangeType ChangeType;
        public bool Stackable;
        public NonStackableType NonStackableLabel;

        [SerializeField]
        private float _amount;
        public float AmountVariantPercentage;
        public float Amount
        {
            get
            {
                LastAmountCrited = MathUtility.RollChance(CriticalChance);
                float amount = _amount * (LastAmountCrited ? CriticalPercentage : 1.0f);
                return amount + Random.Range(-amount * AmountVariantPercentage, amount * AmountVariantPercentage);
            }
        }

        public bool LastAmountCrited
        {
            get; 
            private set; 
        }

        public float RawAmount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public float ChangeDuration;
        public float ChangeInterval = 1.0f;

        public float CriticalChance = 0f;
        public float CriticalPercentage = 2.0f;

        protected override void Deinitialize()
        {
        }
    }
}