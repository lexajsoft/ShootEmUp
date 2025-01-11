using GameManagers;
using Input;
using Zenject;

namespace Commands
{
    public class FinishGameCommand : CommandBase
    {
        private IGameManager _gameManager;
        
        public FinishGameCommand()
        {
            //_gameManager = Zenject.ProjectContext.Instance.Container.Resolve<IGameManager>();
        }
        
        [Inject]        
        public void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void Execute()
        {
            _gameManager.FinishGame();
        }
        
        public class FinishGameCommandFactory
        {
            protected static DiContainer _diContainer;
            
            [Inject]
            public FinishGameCommandFactory(DiContainer diContainer)
            {
                _diContainer = diContainer;
            }
            
            public static FinishGameCommand Create()
            {
                var obj = _diContainer.Instantiate<FinishGameCommand>();
                return obj;
            }
        }
    }
}