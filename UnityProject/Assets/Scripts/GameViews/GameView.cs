
namespace Assets.Scripts.GameViews
{
    public abstract class GameView : GameScripts.GameScript
    {
        protected abstract override void Initialize();

        protected abstract override void Deinitialize();
    }
}
