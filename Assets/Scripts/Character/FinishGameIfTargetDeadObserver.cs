using Commands;
using ShootEmUp;
using UnityEngine;
using GameManager;

namespace Character
{
    /// <summary>
    /// Следит за здоровьем таргета, и если цель умирает передается команда на завершение игры
    /// </summary>
    public class FinishGameIfTargetDeadObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
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
            new FinishGameCommand().Execute();
        }
    }
}