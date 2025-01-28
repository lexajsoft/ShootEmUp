using System;
using Commands;
using GameManagers;
using UnityEngine;
using Zenject;

namespace Enemy
{
    // Убран монобех
    // теперь устанавливается в инсталере
    public class EnemyDestroyObserver : IInitializable, IDisposable
    {
        private EnemyManager _enemyManager;
        private IScoreManager _scoreManager;

        public EnemyDestroyObserver(EnemyManager enemyManager, IScoreManager scoreManager)
        {
            _enemyManager = enemyManager;
            _scoreManager = scoreManager;
        }

        private void OnEnemyWasDestroyed(GameObject obj)
        {
            _scoreManager.AddScore(25);
        }

        void IInitializable.Initialize()
        {
            _enemyManager.OnEnemyWasDestroyed += OnEnemyWasDestroyed;
        }

        void IDisposable.Dispose()
        {
            _enemyManager.OnEnemyWasDestroyed -= OnEnemyWasDestroyed;
        }
    }
}