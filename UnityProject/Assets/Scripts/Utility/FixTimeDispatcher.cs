using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class FixTimeDispatcher
    {
        public event Action DispatchEventHander;
        public float DispatchInterval { get; set; }
        public float DispatchCoolDownPercentage {
            get
            {
                return ((Time.fixedTime - _lastFrameTime) >= DispatchInterval)
                    ? 1.0f
                    : ((Time.fixedTime - _lastFrameTime)/DispatchInterval);
            }
        }
        public bool IsEnabled { get; set; }

        private float _lastFrameTime;

        public FixTimeDispatcher(float dispatchInterval)
        {
            DispatchInterval = dispatchInterval;
            _lastFrameTime = 0f;
            IsEnabled = true;
            ResetTime();
        }

        public void ResetTime () 
        {
            _lastFrameTime = Time.fixedTime;
        }

        public bool CanDispatch()
        {
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
    }
}
