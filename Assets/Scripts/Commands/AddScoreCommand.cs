using GameManager;

namespace Commands
{
    public class AddScoreCommand : CommandBase
    {
        private int _addScore;

        public AddScoreCommand(int addAddScore)
        {
            _addScore = addAddScore;
        }

        public override void Execute()
        {
            ServiceLocator.Get<IScoreManager>().AddScore(_addScore);
        }
    }
}