using Components;
using ShootEmUp;
using UnityEngine;

namespace Bullets
{
    public interface IBulletSystem
    {
        void Shoot(WeaponComponent weaponComponent, TeamComponent teamComponent);
        void Shoot(WeaponComponent weaponComponent, TeamComponent teamComponent, Vector2 directShoot);
        void ShootByPreparedBulletData(PreparedBulletData preparedBulletData);
    }
}