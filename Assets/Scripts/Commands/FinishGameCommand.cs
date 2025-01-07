using GameManager;
using Input;

namespace Commands
{
    public class FinishGameCommand : CommandBase
    {
        public FinishGameCommand()
        {
        }

        public override void Execute()
        {
            ServiceLocator.Get<IGameManager>()?.FinishGame();
        }
    }
}