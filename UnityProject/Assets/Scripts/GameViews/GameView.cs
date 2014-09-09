using Assets.Scripts.GameScripts;

namespace Assets.Scripts.GameViews
{
    public abstract class GameView : GameScript
    {
        protected abstract override void Initialize();

        protected abstract override void Deinitialize();
    }
}
