using System.Collections;
using System.Collections.Generic;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;

        private readonly HashSet<GameObject> m_activeEnemies = new();

        public UnityAction<GameObject> OnEnemyWasDestroyed;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        AddOnEnemyEvents(enemy);
                    }    
                }
            }
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
            }
        }
    }
}