using System;
using Components;
using Facades;
using GameLoop;
using GameLoop.Interfaces;
using Input;
using Installer;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace Character
{
    //public sealed class PlayerController : GameListenerServiceMono<IPlayerController>, IPlayerController, IInitGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    public sealed class PlayerController : MonoBehaviour, IPlayerController, IStartPlayGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener, IInitializable, IDisposable
    {
        private InputManager _inputManager;
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

        [Inject]
        public void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }


        public void StartPlay()
        {
            _inputManager.OnDirectionChanged += _moveComponent.SetDirectToMove;
            _inputManager.OnShoot += _shootFacade.Shoot;
        }

        public void GameFinish()
        {
            _inputManager.OnDirectionChanged -= _moveComponent.SetDirectToMove;
            _inputManager.OnShoot -= _shootFacade.Shoot;
        }

        public void GamePause()
        {
            _inputManager.OnDirectionChanged -= _moveComponent.SetDirectToMove;
            _inputManager.OnShoot -= _shootFacade.Shoot;
        }

        public void GameResume()
        {
            _inputManager.OnDirectionChanged += _moveComponent.SetDirectToMove;
            _inputManager.OnShoot += _shootFacade.Shoot;
        }

        void  IInitializable.Initialize()
        {
            IGameListener.OnRegistry?.Invoke(this);    
        }

        void IDisposable.Dispose()
        {
            IGameListener.OnUnRegistry?.Invoke(this);
        }
        
    }
}