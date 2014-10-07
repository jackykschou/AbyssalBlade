using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/StaticAnimatedSpriteGameView")]
    [RequireComponent(typeof(Animator))]
    public class StaticAnimatedSpriteGameView : StaticSpriteGameView
    {
        protected Animator _animator;

        protected override void Initialize()
        {
            base.Initialize();
            _animator = GetComponent<Animator>();
        }
    }
}
