using GameLoop.Interfaces;

namespace GameLoop
{
    public abstract class GameListenerServiceMono<T> : ServiceMono<T>, IGameListener where T : class
    {
        protected abstract void OnStart();
        
        private void Start()
        {
            IGameListener.OnRegistry?.Invoke(this);
            OnStart();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            IGameListener.OnUnRegistry?.Invoke(this);
        }
    }
}