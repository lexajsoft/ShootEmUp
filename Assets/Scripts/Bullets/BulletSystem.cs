using System;
using System.Collections.Generic;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using Level;
using UnityEngine;
using Zenject;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour, IBulletSystem, ITickGameListener, IInitializable, IDisposable
    {
        [SerializeField] private MainGameLoop _mainGameLoop;
        
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletConfig _bulletConfigPlayer;
        [SerializeField] private BulletConfig _bulletConfigEnemy;
        
        
        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            Debug.Log("BulletSystem.Construct");
            _mainGameLoop = mainGameLoop;
        }

        public void Shoot(WeaponComponent weaponComponent,TeamComponent teamComponent)
        {
            Shoot(weaponComponent,teamComponent,weaponComponent.Direct);
        }
        
        public void Shoot(WeaponComponent weaponComponent,TeamComponent teamComponent, Vector2 directShoot)
        {
            BulletConfigData configData;
            if (teamComponent.IsPlayer)
            {
                configData = _bulletConfigPlayer.GetData();
            }
            else
            {
                configData = _bulletConfigEnemy.GetData();
            }
            
            ShootByPreparedBulletData(new PreparedBulletData()
            {
                isPlayer = teamComponent.IsPlayer,
                physicsLayer = (int) configData.physicsLayer,
                color = configData.color,
                damage = configData.damage,
                position = weaponComponent.Position,
                velocity = directShoot * configData.speed
            });
        }

        public void ShootByPreparedBulletData(PreparedBulletData preparedBulletData)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(prefab, worldTransform);
            }

            
            bullet.SetData(new BulletData()
            {
                position = preparedBulletData.position,
                color = preparedBulletData.color,
                damage = preparedBulletData.damage,
                velocity = preparedBulletData.velocity,
                isPlayer = preparedBulletData.isPlayer,
                physicsLayer = preparedBulletData.physicsLayer,
            });
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, GameObject collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(container);
                _bulletPool.Enqueue(bullet);
            }
        }

        public void GameTick(float deltaTime)
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                bullet.GameTick(deltaTime);
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        private void Start()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(prefab, container);
                _bulletPool.Enqueue(bullet);
            }
        }

        void  IInitializable.Initialize()
        {
            Debug.Log("BulletSystem.Initialize");
            _mainGameLoop.Add(this);
            //IGameListener.OnRegistry.Invoke(this);    
        }

        void IDisposable.Dispose()
        {
            _mainGameLoop.Remove(this);
            //IGameListener.OnUnRegistry.Invoke(this);
        }
    }
}