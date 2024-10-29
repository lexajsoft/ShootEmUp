using Character;
using Commands;
using GameLoop;
using GameLoop.Interfaces;
using GameManager;
using ShootEmUp;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ScreenUIMediator : MediatorBase<ScreenUI>, IGameListenerStart, IGameListenerPause, IGameListenerStop, IGameListenerResume
    {
        private HitPointsComponent _hitPointsComponent;
        private IScoreManager _scoreManager;
        
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

        private void PauseButtonClick()
        {
            new SetStatusGameLoopCommand(GameLoopStatus.GamePause).Execute();
            
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
            _hitPointsComponent = ServiceLocator.Get<ICharacterController>().GetHitPointsComponent();
            _scoreManager = ServiceLocator.Get<IScoreManager>();
        }

        public void GameStart()
        {
            _viewBase.Show();
        }

        public void GamePause()
        {
            _viewBase.Hide();   
        }

        public void GameStop()
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
    }
}