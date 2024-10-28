using System.Collections;
using System.Collections.Generic;
using Common;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public sealed class EnemyManager : GameListenerMono, IGameListenerTick, IGameListenerStart
    {
        [SerializeField] private EnemyPool _enemyPool;

        private readonly HashSet<GameObject> m_activeEnemies = new();

        public UnityAction<GameObject> OnEnemyWasDestroyed;

        private Timer _timer;
        
        private void AddOnEnemyEvents(GameObject enemyGameObject)
        {
            enemyGameObject.GetComponent<HitPointsComponent>().OnIsLiveChanged += OnEnemyDestroyed;
        }

        private void RemoveFromEnemyEvents(GameObject enemyGameObject)
        {
            enemyGameObject.GetComponent<HitPointsComponent>().OnIsLiveChanged += this.OnEnemyDestroyed;
        }
        
        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                RemoveFromEnemyEvents(enemy);
                _enemyPool.UnspawnEnemy(enemy);
                OnEnemyWasDestroyed?.Invoke(enemy);
            }
        }

        public void GameTick(float deltaTime)
        {
            if (_timer.UpdateAndIsChecked(deltaTime))
            {
                var enemy = _enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (m_activeEnemies.Add(enemy))
                    {
                        AddOnEnemyEvents(enemy);
                    }    
                }
            }
        }

        public void GameStart()
        {
            _timer = new Timer(1);
        }
    }
}