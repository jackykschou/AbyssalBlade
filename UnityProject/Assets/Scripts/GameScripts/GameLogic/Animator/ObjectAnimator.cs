using Assets.Scripts.Constants;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    public class ObjectAnimator : GameLogic
    {
        private const int BoolResetBufferFrame = 2;
        private int FrameSinceLastAnimation;

        private UnityEngine.Animator _animator;

        protected override void Initialize()
        {
            base.Initialize();
            FrameSinceLastAnimation = 0;
            _animator = GetComponent<UnityEngine.Animator>();
        }

        [GameLogicEventAttribute(GameLogicEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            _animator.SetInteger(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [GameLogicEventAttribute(GameLogicEvent.OnObjectMove)]
        public void OnObjectMove()
        {
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Move, true);
        }

        public virtual void ResetAllBool()
        {
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Idle, false);
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Move, false);
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Death, false);
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();
            if (FrameSinceLastAnimation > 0)
            {
                FrameSinceLastAnimation--;
                if (FrameSinceLastAnimation == 0)
                {
                    ResetAllBool();
                }
            }
        }
    }
}
