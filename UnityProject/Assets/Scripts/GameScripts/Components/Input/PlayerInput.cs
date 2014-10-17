using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    public class PlayerInput : GameScriptComponent
    {
        [SerializeField]
        private InputKeyCode _keyCode;

        public FixTimeDispatcher CoolDownTimeDispatcher;

        public InputKeyCode KeyCode
        {
            get { return _keyCode; }
            set { _keyCode = value; }
        }

        public virtual bool Detect()
        {
            return !IsInCooldown();
        }

        public override void Initialize()
        {
        }

        public override void Deinitialize()
        {
        }

        protected bool IsInCooldown()
        {
            return !CoolDownTimeDispatcher.CanDispatch();
        }
    }
}
