using Bullets;
using Common;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public sealed class WeaponComponent : GameListenerMono, ITickGameListener
    {
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private Transform _firePoint;
        
        private IBulletSystem _bulletSystem;
        private Timer _timer;

        protected override void OnStart()
        {
            _timer = new Timer(_reloadTime);
            _bulletSystem = ServiceLocator.Get<IBulletSystem>();
        }

        public Vector2 Position => _firePoint.position; 
        public Quaternion Rotation => _firePoint.rotation;
        public Vector3 Direct => _firePoint.up;

        public bool IsCanShoot()
        {
            return _timer.IsCheck();
        }

        public void Cooldown()
        {
            _timer.Reset();
        }

        public void Shoot(TeamComponent teamComponent)
        {
            if (IsCanShoot())
            {
                _bulletSystem.Shoot(this,teamComponent);
                Cooldown();
            }
        }
        
        public void Shoot(TeamComponent teamComponent, Vector2 direct)
        {
            if (IsCanShoot())
            {
                _bulletSystem.Shoot(this,teamComponent,direct);
                Cooldown();
            }
        }

        public void GameTick(float deltaTime)
        {
            _timer.Update(deltaTime);
        }
    }
}