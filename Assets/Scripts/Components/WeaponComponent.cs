using Bullets;
using Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private Transform _firePoint;
        
        private IBulletSystem _bulletSystem;
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(_reloadTime);
        }

        private void Start()
        {
            _bulletSystem = AddictionManager.Instance.Get<IBulletSystem>();
        }

        public Vector2 Position => _firePoint.position; 
        public Quaternion Rotation => this._firePoint.rotation;
        public Vector3 Direct => _firePoint.up;

        private void Update()
        {
            _timer.Update(Time.deltaTime);
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
    }
}