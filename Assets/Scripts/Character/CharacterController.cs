using System;
using Components;
using Facades;
using Input;
using ShootEmUp;
using UnityEngine;

namespace Character
{
    public interface ICharacterController
    {
        HitPointsComponent GetHitPointsComponent();
        MoveComponent GetMoveComponent();
    }

    public interface IRegistry
    {
        public void Registry();
    }

    public sealed class CharacterController : MonoBehaviour, ICharacterController, IRegistry
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private ShootFacade _shootFacade;
        
        
        private void OnEnable()
        {
            _inputManager.OnHorizontalDirectionChanged += _moveComponent.SetDirectToMove;
            _inputManager.OnShoot += _shootFacade.Shoot;
        }

        private void OnDisable()
        {
            _inputManager.OnHorizontalDirectionChanged -= _moveComponent.SetDirectToMove;
            _inputManager.OnShoot -= _shootFacade.Shoot;
        }

        public HitPointsComponent GetHitPointsComponent()
        {
            return _hitPointsComponent;
        }

        public MoveComponent GetMoveComponent()
        {
            return _moveComponent;
        }

        public void Registry()
        {
            AddictionManager.Instance.Registy(typeof(ICharacterController), this);
        }
    }
}