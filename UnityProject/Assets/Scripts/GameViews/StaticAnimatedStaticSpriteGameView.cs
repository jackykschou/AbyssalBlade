using UnityEngine;

namespace Assets.Scripts.GameViews
{
    [RequireComponent(typeof(Animator))]
    public class StaticAnimatedStaticSpriteGameView : StaticSpriteGameView
    {
        protected Animator _animator;

        protected override void Initialize()
        {
            base.Initialize();

            _animator = GetComponent<Animator>();
        }
    }
}
