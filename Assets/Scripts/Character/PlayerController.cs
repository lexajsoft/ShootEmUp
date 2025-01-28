using System;
using Bullets;
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
    public sealed class PlayerController : MonoBehaviour, IPlayerController, IStartPlayGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener, ITickGameListener, IInitializable, IDisposable
    {
        private InputManager _inputManager;
        private MainGameLoop _mainGameLoop;
        
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
        public void Construct(InputManager inputManager, MainGameLoop mainGameLoop, IBulletSystem bulletSystem)
        {
            _inputManager = inputManager;
            _mainGameLoop = mainGameLoop;
            _shootFacade.SetBulletSystem(bulletSystem);
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

        void IInitializable.Initialize()
        {
            _mainGameLoop.Add(this);
            //IGameListener.OnRegistry?.Invoke(this);
        }
        
        void IDisposable.Dispose()
        {
            _mainGameLoop.Remove(this);
            //IGameListener.OnUnRegistry?.Invoke(this);
        }

        public void GameTick(float deltaTime)
        {
            _moveComponent.GameTick(deltaTime);
            _shootFacade.GameTick(deltaTime);
        }
    }
}