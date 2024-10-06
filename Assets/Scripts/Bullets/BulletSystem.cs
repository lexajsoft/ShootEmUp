using System;
using System.Collections.Generic;
using Character;
using Components;
using Level;
using ShootEmUp;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour,IBulletSystem, IRegistry
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletConfig _bulletConfigPlayer;
        [SerializeField] private BulletConfig _bulletConfigEnemy;
        
        
        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }

            
        }

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
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
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            
            bullet.SetData(new BulletData()
            {
                position = preparedBulletData.position,
                color = preparedBulletData.color,
                damage = preparedBulletData.damage,
                velocity = preparedBulletData.velocity,
                isPlayer = preparedBulletData.isPlayer,
                physicsLayer = preparedBulletData.physicsLayer
            });
            
            if (this.m_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, GameObject collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }


        public void Registry()
        {
            AddictionManager.Instance.Registy(typeof(IBulletSystem), this);
        }
    }
}