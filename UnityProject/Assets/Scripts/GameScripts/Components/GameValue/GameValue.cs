using System;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.GameValue
{
    [Serializable]
    public sealed class GameValue : GameScriptComponent
    {
        public float InitialMinValue;
        public float InitialMaxValue;
        public float InitialValue;

        private float _value;

        public float Value {
            get { return _value; }
            set
            {
                if (Frozen)
                {
                    return;
                }
                _value = value;
                TrimValue();
            }
        }

        public int IntValue 
        {
            get { return (int) Value; }
        }

        public float Min { get; private set; }
        public float Max { get; private set; }
        public bool Frozen { get; set; }

        public bool AtMin
        {
            get { return Mathf.Approximately(Value, Min); }
        }

        public bool AtMax
        {
            get { return Mathf.Approximately(Value, Max); }
        }

        public GameValue(float value)
        {
            Frozen = false;
            Min = float.MinValue;
            Max = float.MaxValue;
            Value = value;
        }

        public void SetBound(float min, float max)
        {
            if (min > max)
            {
                throw new Exception("Lower bound cannot be larger than the upper bound");
            }
            else if (max < min)
            {
                throw new Exception("Lower bound cannot be larger than the upper bound");
            }

            Min = min;
            Max = max;
            TrimValue();
        }

        public static implicit operator GameValue(float value)
        {
            return new GameValue(value);
        }

        public static GameValue operator +(GameValue v1, GameValue v2)
        {
            return v1.Value + v2.Value;
        }

        public static float operator +(GameValue v1, float v2)
        {
            return v1.Value + v2;
        }

        public static GameValue operator -(GameValue v1, GameValue v2)
        {
            return v1.Value - v2.Value;
        }

        public static float operator -(GameValue v1, float v2)
        {
            return v1.Value - v2;
        }

        public static GameValue operator *(GameValue v1, GameValue v2)
        {
            return v1.Value * v2.Value;
        }

        public static float operator *(GameValue v1, float v2)
        {
            return v1.Value * v2;
        }

        public static GameValue operator /(GameValue v1, GameValue v2)
        {
            return v1.Value/v2.Value;
        }

        public static float operator /(GameValue v1, float v2)
        {
            return v1.Value / v2; ;
        }

        public static implicit operator float(GameValue v)
        {
            return v.Value;
        }

        public static void ModifyByFixedValue(GameValue gameValue, float value)
        {
            gameValue.Value += gameValue.Value + value;
        }

        public static void ModifyByCurrentPercentage(GameValue gameValue, float percentage)
        {
            gameValue.Value += gameValue.Value * percentage;
        }

        public static void ModifyByMaxPercentage(GameValue gameValue, float percentage)
        {
            gameValue.Value += gameValue.Value * percentage;
        }

        private void TrimValue()
        {
            _value = Mathf.Clamp(_value, Min, Max);
        }

        public override void Initialize()
        {
            if (Mathf.Approximately(InitialMaxValue, 0) && Mathf.Approximately(InitialMinValue, 0) &&
                Mathf.Approximately(InitialValue, 0))
            {
                Min = float.MinValue;
                Max = float.MaxValue;
                Value = 0;
            }
            else
            {
                Frozen = false;
                Min = InitialMinValue;
                Max = InitialMaxValue;
                Value = InitialValue;
                TrimValue();
            }
        }

        public override void Deinitialize()
        {
        }

        public override void EditorUpdate()
        {
            InitialMinValue = Mathf.Clamp(InitialMinValue, float.MinValue, InitialMaxValue);
            InitialMaxValue = Mathf.Clamp(InitialMaxValue, InitialMinValue, float.MaxValue);
            InitialValue = Mathf.Clamp(InitialValue, InitialMinValue, InitialMaxValue);
        }
    }
}
