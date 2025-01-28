using System;
using System.Collections.Generic;
using Bullets;
using Character;
using Commands;
using Enemy;
using GameLoop;
using GameManagers;
using Input;
using Level;
using UI.Screens;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installer
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private ScreenManager _screenManager;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private LevelBackground _levelBackground;
        public override void InstallBindings()
        {

            // надо вызывать именно так чтобы он создался и подписался на IGameListener
            var mainGameLoop = new MainGameLoop();
            
            Container.Bind<IScoreManager>().To<ScoreManager>().AsCached().NonLazy();
            Container.Bind<IGameManager>().To<GameManager>().AsCached().NonLazy();
            
            // Bullet System
            Container.Bind(typeof(IBulletSystem), typeof(IInitializable), typeof(IDisposable)).To<BulletSystem>().FromInstance(_bulletSystem).AsCached();
            // Enemys
            Container.BindInterfacesAndSelfTo<EnemyPool>().FromInstance(_enemyPool).AsCached();
            Container.Bind(typeof(EnemyManager), typeof(IInitializable), typeof(IDisposable)).To<EnemyManager>().FromInstance(_enemyManager).AsCached();
            // PlayerController
            Container.BindInterfacesTo<PlayerController>().FromInstance(playerController).AsCached();
            // ScreenManager
            Container.BindInterfacesTo<ScreenManager>().FromInstance(_screenManager).AsCached();
            // InputManager
            Container.BindInterfacesAndSelfTo<InputManager>().AsCached().NonLazy();
            // LevelBackground
            Container.Bind(typeof(LevelBackground), typeof(IInitializable), typeof(IDisposable)).To<LevelBackground>().FromInstance(_levelBackground).AsCached();

            Container.Bind<ScreenMenuFinish>().To<ScreenMenuFinish>().AsCached();

            //Container.Bind<AddScoreCommand.AddScoreCommandFactory>().AsSingle().NonLazy();
            Container.Bind(typeof(EnemyDestroyObserver), typeof(IInitializable), typeof(IDisposable)).To<EnemyDestroyObserver>().AsCached().NonLazy();
            
            //Container.Bind<FinishGameCommand.FinishGameCommandFactory>().AsSingle().NonLazy();

            // !!! MainGameLoop !!!
            Container.BindInterfacesAndSelfTo<MainGameLoop>().FromInstance(mainGameLoop).AsCached(); 
        }
    }
}