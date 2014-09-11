using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.Input
{
    public class PlayerInput : SerializableComponent
    {
        [SerializeField]
        private InputKeyCode _keyCode;
        [SerializeField]
        protected float CoolDown;

        protected FixTimeDispatcher CoolDownTimeDispatcher;

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
            CoolDownTimeDispatcher = new FixTimeDispatcher(CoolDown);
        }

        public override void Deinitialize()
        {
        }

        public override void Update()
        {
        }

        protected bool IsInCooldown()
        {
            return !CoolDownTimeDispatcher.CanDispatch();
        }
    }
}
