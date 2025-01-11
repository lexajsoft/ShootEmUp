using System;
using System.Collections.Generic;
using Bullets;
using Character;
using Commands;
using Enemy;
using GameLoop;
using GameManagers;
using Input;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class Installer : MonoInstaller
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private ScreenManager _screenManager;
        [SerializeField] private EnemyPool _enemyPool;
        
        public override void InstallBindings()
        {
            // надо вызывать именно так чтобы он создался и подписался на IGameListener
            var mainGameLoop = new MainGameLoop();
            
            Container.Bind<IScoreManager>().To<ScoreManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<IGameManager>().To<GameManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<IBulletSystem>().To<BulletSystem>().FromInstance(_bulletSystem).AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
            Container.Bind<ScreenManager>().FromInstance(_screenManager).AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainGameLoop>().FromInstance(mainGameLoop).AsSingle();

            Container.Bind<AddScoreCommand.AddScoreCommandFactory>().AsSingle().NonLazy();
            Container.Bind<FinishGameCommand.FinishGameCommandFactory>().AsSingle().NonLazy();
        }
    }
}