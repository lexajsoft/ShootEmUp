using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Common;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour,IInitializable, IDisposable,  ITickGameListener, IInitGameListener
    {
        [SerializeField] private EnemyPool _enemyPool;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();
        public UnityAction<GameObject> OnEnemyWasDestroyed;
        private Timer _timer;
        private MainGameLoop _mainGameLoop;
        private IBulletSystem _bulletSystem;
        private Dictionary<GameObject, EnemyUpdater> _enemyUpdaters = new Dictionary<GameObject, EnemyUpdater>();
        
        [Inject]
        public void Construct(MainGameLoop mainGameLoop, IBulletSystem bulletSystem)
        {
            _mainGameLoop = mainGameLoop;
            _bulletSystem = bulletSystem;
        }

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
                
                _enemyUpdaters.Remove(enemy);
            }
        }

        public void GameTick(float deltaTime)
        {
            if (_timer.UpdateAndIsChecked(deltaTime))
            {
                // может придти null изза отсутсвия в пуле объектов
                var enemy = _enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    // добавление противника в список на обновление
                    var enemyUpdater = new EnemyUpdater();
                    enemyUpdater.SetEnemyGameObject(enemy);
                    enemyUpdater.SetBulletSystem(_bulletSystem);
                    _enemyUpdaters.Add(enemy, enemyUpdater);
                    
                    if (m_activeEnemies.Add(enemy))
                    {
                        AddOnEnemyEvents(enemy);
                    }    
                }
            }

            foreach (var enemyItem in _enemyUpdaters)
            {
                enemyItem.Value.GameTick(deltaTime);
            }
        }

        public void GameInit()
        {
            _timer = new Timer(1);
        }

        void IInitializable.Initialize()
        {
            _mainGameLoop.Add(this);
        }

        void IDisposable.Dispose()
        {
            _mainGameLoop.Remove(this);
        }
    }
}