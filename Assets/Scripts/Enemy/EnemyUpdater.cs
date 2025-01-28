using Bullets;
using Enemy.Agents;
using GameLoop.Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyUpdater : ITickGameListener
    {
        private GameObject _gameObject;
        private EnemyAttackAgent _enemyAttackAgent;
        private EnemyMoveAgent _enemyMoveAgent;
    
        public void SetEnemyGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
            _enemyAttackAgent = _gameObject.GetComponent<EnemyAttackAgent>();
            _enemyMoveAgent = _gameObject.GetComponent<EnemyMoveAgent>();
        }

        public void GameTick(float deltaTime)
        {
            _enemyAttackAgent.GameTick(deltaTime);
            _enemyMoveAgent.GameTick(deltaTime);
        }

        public void SetBulletSystem(IBulletSystem bulletSystem)
        {
            _enemyAttackAgent.SetBulletSystem(bulletSystem);
        }
    }
}
