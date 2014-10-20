using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.GameValue
{
    [Serializable]
    public sealed class GameValue : GameScriptComponent
    {
        [Serializable]
        public class GameValueTempChanger : GameScriptComponent
        {

            public float ChangeAmount;
            public bool CountdownChangeTime;
            public float ChangeTime;

            public void TempChangeGameValue(GameValue gameValue)
            {
                if (CountdownChangeTime)
                {
                }
                else
                {
                    
                }
            }

            public override void Initialize()
            {
            }

            public override void Deinitialize()
            {
            }

            public override void EditorUpdate()
            {
                base.EditorUpdate();
                ChangeTime = Mathf.Clamp(ChangeTime, 0f, 100f);
            }
        }

        public float InitialMinValue;
        public float InitialMaxValue;
        public float InitialValue;

        private float _value;
        private Dictionary<GameValueTempChanger, float> _valueTempChangeEntitiesMap;
        private Dictionary<GameValueTempChanger, float> _minTempChangeEntitiesMap;
        private Dictionary<GameValueTempChanger, float> _maxTempChangeEntitiesMap;

        public float Value {
            get { return _value; }
            set
            {
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
            _valueTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            _minTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            _maxTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            Min = float.MinValue;
            Max = float.MaxValue;
            Value = value;
        }

        public GameValue(float value, float min, float max) : this(value)
        {
            SetBound(min, max);
        }

        public void SetBound(float min, float max)
        {
            Min = (min > Mathf.Max(Max, max)) ? max : min;
            Max = (max < Min) ? Min : min;
            TrimValue();
        }

        public static explicit operator GameValue(float value)
        {
            return new GameValue(value);
        }

        public static GameValue operator +(GameValue v1, GameValue v2)
        {
            return new GameValue(v1.Value + v2.Value, v1.Min, v1.Max);
        }

        public static float operator +(GameValue v1, float v2)
        {
            return new GameValue(v1.Value + v2, v1.Min, v1.Max);
        }

        public static GameValue operator -(GameValue v1, GameValue v2)
        {
            return new GameValue(v1.Value - v2.Value, v1.Min, v1.Max);
        }

        public static float operator -(GameValue v1, float v2)
        {
            return new GameValue(v1.Value - v2, v1.Min, v1.Max);
        }

        public static GameValue operator *(GameValue v1, GameValue v2)
        {
            return new GameValue(v1.Value * v2.Value, v1.Min, v1.Max);
        }

        public static float operator *(GameValue v1, float v2)
        {
            return new GameValue(v1.Value * v2, v1.Min, v1.Max);
        }

        public static GameValue operator /(GameValue v1, GameValue v2)
        {
            return new GameValue(v1.Value / v2.Value, v1.Min, v1.Max);
        }

        public static float operator /(GameValue v1, float v2)
        {
            return new GameValue(v1.Value / v2, v1.Min, v1.Max);
        }

        public static implicit operator float(GameValue v)
        {
            return v.Value;
        }

        private void TrimValue()
        {
            _value = Mathf.Clamp(_value, Min, Max);
        }

        public override void Initialize()
        {
            _valueTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            _minTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            _maxTempChangeEntitiesMap = new Dictionary<GameValueTempChanger, float>();
            if (Mathf.Approximately(InitialMaxValue, 0) && Mathf.Approximately(InitialMinValue, 0) &&
                Mathf.Approximately(InitialValue, 0))
            {
                Min = float.MinValue;
                Max = float.MaxValue;
                Value = 0;
            }
            else
            {
                Min = InitialMinValue;
                Max = InitialMaxValue;
                Value = InitialValue;
                TrimValue();
            }
        }

        public override void Deinitialize()
        {
        }

        public void TempChangeValueByFixedAmount(float amount, float time)
        {
            GameScript.StartCoroutine(TempChangeValue(amount, time));
        }
        public void TempChangeValueByCurrentPercentage(float percentage, float time)
        {
            TempChangeValueByFixedAmount(Value * percentage, time);
        }

        public void TempChangeValueByMaxPercentage(float percentage, float time)
        {
            TempChangeValueByFixedAmount(Max * percentage, time);
        }

        private IEnumerator TempChangeValue(float amount, float time)
        {
            Value += amount;
            yield return new WaitForSeconds(time);
            Value -= amount;
        }

        public void TempChangeMaxByFixedAmount(float amount, float time)
        {
            GameScript.StartCoroutine(TempChangeMax(amount, time));
        }

        private IEnumerator TempChangeMax(float amount, float time)
        {
            SetBound(Min, Max + amount);
            yield return new WaitForSeconds(time);
            SetBound(Min, Max - amount);
        }

        public void TempChangeMinByFixedAmount(float amount, float time)
        {
            GameScript.StartCoroutine(TempChangeMin(amount, time));
        }

        private IEnumerator TempChangeMin(float amount, float time)
        {
            SetBound(Min + amount, Max);
            yield return new WaitForSeconds(time);
            SetBound(Min - amount, Max);
        }

        public void TempChangeValueByFixedAmount(GameValueTempChanger entity, float amount)
        {
            if (_valueTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            Value += amount;
            _valueTempChangeEntitiesMap.Add(entity, amount);
        }

        public void TempChangeValueByCurrentPercentage(GameValueTempChanger entity, float percentage)
        {
            TempChangeValueByFixedAmount(entity, Value * percentage);
        }

        public void TempChangeValueByMaxPercentage(GameValueTempChanger entity, float percentage)
        {
            TempChangeValueByFixedAmount(entity, Max * percentage);
        }

        public void UnchangeTempChangedValue(GameValueTempChanger entity)
        {
            if (!_valueTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            Value -= _valueTempChangeEntitiesMap[entity];
            _valueTempChangeEntitiesMap.Remove(entity);
        }

        public void TempChangeMinByFixedAmount(GameValueTempChanger entity, float amount)
        {
            if (_minTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            SetBound(Min + amount, Max);
            _minTempChangeEntitiesMap.Add(entity, amount);
        }

        public void UnchangeTempChangedMin(GameValueTempChanger entity)
        {
            if (!_minTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            SetBound(Min - _maxTempChangeEntitiesMap[entity], Max);
            _minTempChangeEntitiesMap.Remove(entity);
        }

        public void TempChangeMaxByFixedAmount(GameValueTempChanger entity, float amount)
        {
            if (_maxTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            SetBound(Min, Max + amount);
            _maxTempChangeEntitiesMap.Add(entity, amount);
        }

        public void UnchangeTempChangedMax(GameValueTempChanger entity)
        {
            if (!_maxTempChangeEntitiesMap.ContainsKey(entity))
            {
                return;
            }
            SetBound(Min, Max - _maxTempChangeEntitiesMap[entity]);
            _maxTempChangeEntitiesMap.Remove(entity);
        }

        public override void EditorUpdate()
        {
            InitialMinValue = Mathf.Clamp(InitialMinValue, float.MinValue, InitialMaxValue);
            InitialMaxValue = Mathf.Clamp(InitialMaxValue, InitialMinValue, float.MaxValue);
            InitialValue = Mathf.Clamp(InitialValue, InitialMinValue, InitialMaxValue);
        }
    }
}
