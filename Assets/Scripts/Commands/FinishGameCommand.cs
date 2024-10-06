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
            AddictionManager.Instance.Get<IGameManager>()?.FinishGame();            
        }
    }
}