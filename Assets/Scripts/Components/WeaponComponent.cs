using Bullets;
using Common;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour, ITickGameListener
    {
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private Transform _firePoint;
        
        private IBulletSystem _bulletSystem;
        private Timer _timer;
        public Vector2 Position => _firePoint.position; 
        public Quaternion Rotation => _firePoint.rotation;
        public Vector3 Direct => _firePoint.up;

        
        private void Start()
        {
            _timer = new Timer(_reloadTime);
        }

        public void SetBulletSystem(IBulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

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