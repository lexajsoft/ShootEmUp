using Character;
using Commands;
using GameLoop;
using GameLoop.Interfaces;
using GameManagers;
using ShootEmUp;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class ScreenUIMediator : MediatorBase<ScreenUI>, IInitGameListener, IPauseGameListener, IFinishGameListener, IResumeGameListener
    {
        private HitPointsComponent _hitPointsComponent;

        private IScoreManager _scoreManager;
        private MainGameLoop _mainGameLoop;
        private IPlayerController _playerController;
        
        private DiContainer _container;
        
        [Inject]
        public void Constructor(MainGameLoop mainGameLoop, IScoreManager scoreManager, IPlayerController playerController)
        {
            _mainGameLoop = mainGameLoop;
            _scoreManager = scoreManager;
            _playerController = playerController;
        }
        
        
        // [Inject]
        // public void Construct(Zenject.Context context)
        // {
        //     _container = context.Container;
        //     // _mainGameLoop = mainGameLoop;
        //     // _scoreManager = scoreManager;
        //     // _playerController = playerController;
        // }

        // private void Resolve()
        // {
        //     _mainGameLoop = _container.Resolve<MainGameLoop>();
        //     _scoreManager = _container.Resolve<IScoreManager>();
        //     _playerController = _container.Resolve<IPlayerController>();
        // }

        public override void Notify()
        {
            _viewBase.SetHitPoints(_hitPointsComponent.GetHitPoints());
            _viewBase.SetScore(_scoreManager.GetScore());
        }

        public override void Subscribes()
        {
            _hitPointsComponent.OnHitPointsChanged += _viewBase.SetHitPoints;
            _scoreManager.OnScoreChanged += _viewBase.SetScore;
            Notify();

            _viewBase.OnPauseButtonClick += PauseButtonClick;
        }

        public override void Describes()
        {
            _hitPointsComponent.OnHitPointsChanged -= _viewBase.SetHitPoints;
            _scoreManager.OnScoreChanged -= _viewBase.SetScore;
            _viewBase.OnPauseButtonClick -= PauseButtonClick;
        }

        protected override void Disable()
        {
            
        }

        protected override void Enable()
        {
            //Resolve();
            _hitPointsComponent =_playerController.GetHitPointsComponent();
        }

        public void GameInit()
        {
            _viewBase.Show();
        }

        public void GamePause()
        {
            _viewBase.Hide();   
        }

        public void GameFinish()
        {
            _viewBase.Hide();
        }

        // public void GameNone()
        // {
        //     _viewBase.Hide();
        // }

        public void GameResume()
        {
            _viewBase.Show();
        }
        
        private void PauseButtonClick()
        {
            // new SetStatusGameLoopCommand(GameLoopStatus.GamePause).Execute();
            _mainGameLoop.SetStatus(GameLoopStatus.GamePause);
            
        }
    }
}