using System;
using Bullets;
using Components;
using ShootEmUp;
using UnityEngine;

namespace Facades
{
    public class ShootFacade : MonoBehaviour
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
    }
}