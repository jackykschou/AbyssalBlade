using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValue
{
    [AddComponentMenu("Misc/GameValueChanger")]
    public class GameValueChanger : GameLogic
    {
        public class GameValueChangerData
        {
            public ChangeTargetValueType TargetValueType;
            public OneTimeChangeDurationType OneTimeDurationType;
            public ByIntervalChangeDurationType IntervalDurationType;
            public CurrentValueChangeType ChangeType;
            public bool Stackable;
            public NonStackableType NonStackableLabel;

            public float _amount;
            public float AmountVariantPercentage;
            public float Amount
            {
                get
                {
                    LastAmountCrited = UtilityFunctions.RollChance(CriticalChance);
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
        }

        public GameValueChangerData CreateGameValueChangerData()
        {
            GameValueChangerData gameValueChanger = new GameValueChangerData
            {
                TargetValueType = TargetValueType,
                OneTimeDurationType = OneTimeDurationType,
                IntervalDurationType = IntervalDurationType,
                ChangeType = ChangeType,
                Stackable = Stackable,
                NonStackableLabel = NonStackableLabel,
                _amount = _amount,
                AmountVariantPercentage = AmountVariantPercentage,
                ChangeDuration = ChangeDuration,
                ChangeInterval = ChangeInterval,
                CriticalChance = CriticalChance,
                CriticalPercentage = CriticalPercentage
            };

            return gameValueChanger;
        }

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

        public float _amount;
        public float AmountVariantPercentage;
        public float Amount
        {
            get
            {
                LastAmountCrited = UtilityFunctions.RollChance(CriticalChance);
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

        private float _initialChangeDuration;
        private float _initialChangeInterval;
        private float _initialCriticalChance;
        private float _initialCriticalPercentage;
        private float _initialAmount;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _initialChangeDuration = ChangeDuration;
            _initialChangeInterval = ChangeInterval;
            _initialCriticalChance = CriticalChance;
            _initialCriticalPercentage = CriticalPercentage;
            _initialAmount = _amount;
        }

        protected override void Initialize()
        {
            base.Initialize();
            ChangeDuration = _initialChangeDuration;
            ChangeInterval = _initialChangeInterval;
            CriticalChance = _initialCriticalChance;
            CriticalPercentage = _initialCriticalPercentage;
            _amount = _initialAmount;
        }

        protected override void Deinitialize()
        {
        }
    }
}