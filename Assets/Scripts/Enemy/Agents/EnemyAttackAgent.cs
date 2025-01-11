using Components;
using GameLoop;
using GameLoop.Interfaces;
using ShootEmUp;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : GameListenerMono, ITickGameListener
    {
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        
        private GameObject target;
        
        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) target.transform.position - startPosition;
            var direction = vector.normalized;
            _weaponComponent.Shoot(_teamComponent, direction);
        }

        public void GameTick(float deltaTime)
        {
            if (!_moveAgent.IsReached)
            {
                return;
            }
            
            if (!target.GetComponent<HitPointsComponent>().IsLive())
            {
                return;
            }

            if(_weaponComponent.IsCanShoot())
            {
                Fire();
            }
        }
    }
}