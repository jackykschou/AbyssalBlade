﻿using System;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.TimeDispatcher
{
    [Serializable]
    public class FixTimeDispatcher : GameScriptComponent
    {
        public bool IsEnabled;
        public event Action DispatchEventHander;
        public float DispatchInterval;
        public float DispatchCoolDownPercentage {
            get
            {
                return ((Time.fixedTime - _lastFrameTime) >= DispatchInterval)
                    ? 1.0f
                    : ((Time.fixedTime - _lastFrameTime) / DispatchInterval);
            }
        }

        private float _lastFrameTime;

        public void ResetTime () 
        {
            _lastFrameTime = Time.time;
        }

        public bool CanDispatch()
        {
            if (DispatchInterval <= 0f)
            {
                return true;
            }

            return ((Time.fixedTime - _lastFrameTime) >= DispatchInterval) && IsEnabled;
        }

        public bool Dispatch()
        {
            if (CanDispatch())
            {
                if (DispatchEventHander != null)
                {
                    DispatchEventHander();
                }
                ResetTime();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Initialize()
        {
            IsEnabled = true;
            _lastFrameTime = -100;
        }

        public override void Deinitialize()
        {
        }
    }
}
