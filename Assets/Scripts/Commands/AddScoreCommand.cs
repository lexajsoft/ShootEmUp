using GameManagers;
using Zenject;

namespace Commands
{
    public class AddScoreCommand : CommandBase
    {
        public static AddScoreCommandFactory addScoreCommandFactory;
        private int _addScore;
        private IScoreManager _scoreManager;
        
        public AddScoreCommand()
        {
            _addScore = 0;
        }

        public AddScoreCommand(int addAddScore)
        {
            _addScore = addAddScore;
        }

        [Inject]
        public void Construct(IScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        public override void Execute()
        {
            
            _scoreManager.AddScore(_addScore);
        }

        public class AddScoreCommandFactory
        {
            protected static DiContainer _diContainer;
            
            [Inject]
            public AddScoreCommandFactory(DiContainer diContainer)
            {
                _diContainer = diContainer;
            }
            
            public static AddScoreCommand Create(int value)
            {
                // забавно но всеравно лезет к конструктору который без аргументов
                //var obj = _diContainer.Instantiate<AddScoreCommand>(new object[]{value});
                
                var obj = _diContainer.Instantiate<AddScoreCommand>();
                obj._addScore = value;
                return obj;
            }
        }
    }
}