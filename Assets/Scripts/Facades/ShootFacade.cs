using System;
using Bullets;
using Components;
using GameLoop.Interfaces;
using ShootEmUp;
using UnityEngine;

namespace Facades
{
    public class ShootFacade : MonoBehaviour , ITickGameListener
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private TeamComponent _teamComponent;

        public void Shoot()
        {
            _weaponComponent.Shoot(_teamComponent);
        }
        public void Shoot(Vector2 direct)
        {
            _weaponComponent.Shoot(_teamComponent,direct);
        }

        public void GameTick(float deltaTime)
        {
            _weaponComponent.GameTick(deltaTime);
        }

        public void SetBulletSystem(IBulletSystem bulletSystem)
        {
            _weaponComponent.SetBulletSystem(bulletSystem);
        }
    }
}