using GameLoop;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace Character
{
    /// <summary>
    /// Следит за здоровьем таргета, и если цель умирает передается команда на завершение игры
    /// </summary>
    public class FinishGameObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        private MainGameLoop _mainGameLoop;

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }
        
        private void OnEnable()
        {
            _hitPointsComponent.OnIsLiveChanged += OnTargetIsDead;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnIsLiveChanged -= OnTargetIsDead;
        }
        
        void OnTargetIsDead(GameObject _)
        {
            _mainGameLoop.SetStatus(GameLoopStatus.GameFinish);
        }
    }
}