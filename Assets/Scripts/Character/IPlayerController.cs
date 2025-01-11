using Components;
using ShootEmUp;

namespace Character
{
    public interface IPlayerController
    {
        HitPointsComponent GetHitPointsComponent();
        MoveComponent GetMoveComponent();
    }
}