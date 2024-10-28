using Components;
using ShootEmUp;

namespace Character
{
    public interface ICharacterController
    {
        HitPointsComponent GetHitPointsComponent();
        MoveComponent GetMoveComponent();
    }
}