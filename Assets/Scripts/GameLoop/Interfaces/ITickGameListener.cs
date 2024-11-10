namespace GameLoop.Interfaces
{
    public interface ITickGameListener : IGameListener
    {
        void GameTick(float deltaTime);
    }
}