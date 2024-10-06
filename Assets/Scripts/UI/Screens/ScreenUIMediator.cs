using Character;
using GameManager;
using ShootEmUp;

namespace UI.Screens
{
    public class ScreenUIMediator : MediatorBase<ScreenUI>
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
        }

        public override void Describes()
        {
            _hitPointsComponent.OnHitPointsChanged -= _viewBase.SetHitPoints;
            _scoreManager.OnScoreChanged += _viewBase.SetScore;
        }

        protected override void Disable()
        {
        }

        protected override void Enable()
        {
            _hitPointsComponent = AddictionManager.Instance.Get<ICharacterController>().GetHitPointsComponent();
            _scoreManager = AddictionManager.Instance.Get<IScoreManager>();
        }
    }
}