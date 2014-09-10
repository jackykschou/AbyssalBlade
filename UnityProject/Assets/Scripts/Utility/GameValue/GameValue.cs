using System;
using UnityEngine;

namespace Assets.Scripts.Utility.GameValue
{
    public sealed class GameValue
    {
        private float _value;

        public float Value {
            get { return Value; }
            set
            {
                if (Frozen)
                {
                    return;;
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

        public GameValue()
        {
            Value = 0f;
            Min = float.MinValue;
            Max = float.MaxValue;
            Frozen = false;
        }

        public GameValue(float value)
        {
            Value = value;
            Min = float.MinValue;
            Max = float.MaxValue;
            Frozen = false;
        }

        public GameValue(float value, float min, float max)
        {
            Value = value;
            Min = min;
            Max = max;
            Frozen = false;
            TrimValue();
        }

        public void SetBound(float min, float max)
        {
            if (min > max)
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
            Value = Mathf.Clamp(Value, Min, Max);
        }
    }
}
