using System;
using Components;
using Facades;
using GameLoop;
using GameLoop.Interfaces;
using Input;
using Installer;
using ShootEmUp;
using UnityEngine;

namespace Character
{
    public sealed class CharacterController : GameListenerServiceMono<ICharacterController>, ICharacterController, IInitGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private ShootFacade _shootFacade;

        public HitPointsComponent GetHitPointsComponent()
        {
            return _hitPointsComponent;
        }

        public MoveComponent GetMoveComponent()
        {
            return _moveComponent;
        }

        
        
        public void GameInit()
        {
            //ServiceLocator.Registy(typeof(ICharacterController), this);
            _inputManager.OnHorizontalDirectionChanged += _moveComponent.SetDirectToMove;
            _inputManager.OnShoot += _shootFacade.Shoot;
        }

        public void GameFinish()
        {
            _inputManager.OnHorizontalDirectionChanged -= _moveComponent.SetDirectToMove;
            _inputManager.OnShoot -= _shootFacade.Shoot;
        }

        public void GamePause()
        {
            _inputManager.OnHorizontalDirectionChanged -= _moveComponent.SetDirectToMove;
            _inputManager.OnShoot -= _shootFacade.Shoot;
        }

        public void GameResume()
        {
            _inputManager.OnHorizontalDirectionChanged += _moveComponent.SetDirectToMove;
            _inputManager.OnShoot += _shootFacade.Shoot;
        }

        protected override void OnStart()
        {
            
        }
    }
}