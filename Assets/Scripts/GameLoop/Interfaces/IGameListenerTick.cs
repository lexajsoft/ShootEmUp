namespace GameLoop.Interfaces
{
    public interface IGameListenerTick : IGameListener
    {
        void GameTick(float deltaTime);
    }
}