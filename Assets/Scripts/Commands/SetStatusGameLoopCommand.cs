namespace Commands
{
    public class SetStatusGameLoopCommand : CommandBase
    {
        private readonly GameLoop.GameLoopStatus _status;

        public SetStatusGameLoopCommand(GameLoop.GameLoopStatus status)
        {
            _status = status;
        }

        public override void Execute()
        {
            var gameLoop = ServiceLocator.Get<GameLoop.GameLoop>();
            gameLoop.SetStatus(_status);
        }
    }
}