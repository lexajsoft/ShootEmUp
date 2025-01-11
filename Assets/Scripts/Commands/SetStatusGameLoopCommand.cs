using Zenject;

namespace Commands
{
    // TODO уже не используется
    public class SetStatusGameLoopCommand : CommandBase
    {
        private readonly GameLoop.GameLoopStatus _status;
        private GameLoop.MainGameLoop _mainGameLoop;
        public SetStatusGameLoopCommand(GameLoop.GameLoopStatus status)
        {
            _status = status;
        }

        [Inject]
        public void Construct(GameLoop.MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }
        
        public override void Execute()
        {
            _mainGameLoop.SetStatus(_status);
        }
    }
}