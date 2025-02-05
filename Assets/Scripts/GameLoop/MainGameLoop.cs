using System;
using System.Collections.Generic;
using GameLoop.Interfaces;
using UnityEngine;
using Zenject;

namespace GameLoop
{
    [Serializable]
    public enum GameLoopStatus
    {
        GameInit,
        GamePlay,
        GamePause,
        GameResume,
        GameFinish
    }

    // public class MainGameLoop : ITickable, IInitializable, IDisposable
    // IInitializable - убран отсюда так как начинает конфликтовать во время биндинга,
    // ибо инициализация должна быть после всех подписок,
    // а она вызывается в самом начале и в итоге ничего не работает =(
    [Serializable]
    public class MainGameLoop : IInitializable, ITickable, IDisposable //: ServiceMono<GameLoop>
    {
        [SerializeField] private GameLoopStatus _gameLoopStatus = GameLoopStatus.GameInit;

        // порядок включения
        // init -> (resume -> playing) -> finish
        // init -> (resume -> playing) -> pause -> (resume -> playing)-> finish
        
        private List<IInitGameListener> _initGameListeners;
        private List<IStartPlayGameListener> _startPlayGameListeners;
        private List<IResumeGameListener> _resumeGameListeners;
        private List<ITickGameListener> _tickGameListeners;
        private List<IPauseGameListener> _pauseGameListeners;
        private List<IFinishGameListener> _finishGameListeners;

        private static int countCreated = 0;
        
        public MainGameLoop()
        {
            //Debug.Log($"[{this.GetType().Name}]:" + " Constructor:" + countCreated++.ToString());
            
            _initGameListeners = new List<IInitGameListener>();
            _startPlayGameListeners = new List<IStartPlayGameListener>();
            _resumeGameListeners = new List<IResumeGameListener>();
            _tickGameListeners = new List<ITickGameListener>();
            _pauseGameListeners = new List<IPauseGameListener>();
            _finishGameListeners = new List<IFinishGameListener>();
            
            // IGameListener.OnRegistry += OnGameListenerOnRegistry;
            // IGameListener.OnUnRegistry += OnGameListenerUnOnRegistry;
        }

        public void InitGame()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(InitGame));
            
            _gameLoopStatus = GameLoopStatus.GameInit; 
            UpdateStatus();
        }

        private void OnGameListenerOnRegistry(IGameListener obj) => RegistryGameListener(obj);
        private void OnGameListenerUnOnRegistry(IGameListener obj) => RegistryGameListener(obj);

        //protected override void OnDestroy()
        protected void DeInitGame()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(DeInitGame));
            //base.OnDestroy();
            // IGameListener.OnRegistry -= OnGameListenerOnRegistry;
            // IGameListener.OnUnRegistry -= OnGameListenerUnOnRegistry;
        }

        public void RegistryGameListener(IGameListener obj)
        {
            Debug.Log("Try registry:" + obj.GetType().Name);
            if (obj is not null)
            {
                Add(obj);
            }
        }
        
        public void UnRegistryGameListener(IGameListener obj)
        {
            if (obj is not null)
            {
                Remove(obj);
            }
        }

        public void SetStatus(GameLoopStatus gameLoopStatus)
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(SetStatus) + $" [{gameLoopStatus.ToString()}]");
            if(_gameLoopStatus == gameLoopStatus)
                return;
#if UNITY_EDITOR
            Debug.Log("SetStatus:" + _gameLoopStatus.ToString() +  "=>" + gameLoopStatus.ToString());
#endif            
            _gameLoopStatus = gameLoopStatus;
            UpdateStatus();
            
        }

        private void UpdateStatus()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(UpdateStatus) + $" [{_gameLoopStatus.ToString()}]");
            // убирает перевызывание одного и того же статуса
            switch (_gameLoopStatus)
            {
                case GameLoopStatus.GameInit:
                {
                    GameInit();
                    break;
                }
                case GameLoopStatus.GamePlay:
                {
                    GamePlay();
                    break;
                }
                case GameLoopStatus.GamePause:
                {
                    GamePause();
                    break;
                }
                case GameLoopStatus.GameResume:
                {
                    GameResume();
                    break;
                }
                case GameLoopStatus.GameFinish:
                {
                    GameFinish();
                    break;
                }
            }
        }

        private void GameInit()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(GameInit));
            
            for (int i = 0; i < _initGameListeners.Count; i++)
            {
                _initGameListeners[i].GameInit();
            }

            //_gameLoopStatus = GameLoopStatus.GamePlay;
        }

        private void GameFinish()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(GameFinish));
            for (int i = 0; i < _finishGameListeners.Count; i++)
            {
                _finishGameListeners[i].GameFinish();
            }
        }

        private void GameResume()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(GameResume));
            for (int i = 0; i < _resumeGameListeners.Count; i++)
            {
                _resumeGameListeners[i].GameResume();
            }

            SetStatus(GameLoopStatus.GamePlay);
            UpdateStatus();
        }

        private void GamePause()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(GamePause));
            for (int i = 0; i < _pauseGameListeners.Count; i++)
            {
                _pauseGameListeners[i].GamePause();
            }
        }
        
        private void GamePlay()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(GamePlay));
            for (int i = 0; i < _startPlayGameListeners.Count; i++)
            {
                _startPlayGameListeners[i].StartPlay();
            }
        }

        public void Add(IGameListener obj)
        {
            Debug.Log("Registry:" + obj.GetType().Name);
            if (obj is IInitGameListener initGameListener)
            {
                _initGameListeners.Add(initGameListener);
            }

            if (obj is IStartPlayGameListener startPlayGameListener)
            {
                _startPlayGameListeners.Add(startPlayGameListener);
            }

            if (obj is IFinishGameListener finishGameListener)
            {
                _finishGameListeners.Add(finishGameListener);
            }
            
            if (obj is ITickGameListener tickGameListener)
            {
                _tickGameListeners.Add(tickGameListener);
            }
            if (obj is IPauseGameListener pauseGameListener)
            {
                _pauseGameListeners.Add(pauseGameListener);
            }
            
            if (obj is IResumeGameListener resumeGameListener)
            {
                _resumeGameListeners.Add(resumeGameListener);
            }
        }
        
        public void Remove(IGameListener obj)
        {
            if (obj is IInitGameListener initGameListener)
            {
                _initGameListeners.Remove(initGameListener);
            }
            
            if (obj is IStartPlayGameListener startPlayGameListener)
            {
                _startPlayGameListeners.Remove(startPlayGameListener);
            }
            
            if (obj is IFinishGameListener finishGameListener)
            {
                _finishGameListeners.Remove(finishGameListener);
            }
            
            if (obj is ITickGameListener tickGameListener)
            {
                _tickGameListeners.Remove(tickGameListener);
            }
            if (obj is IPauseGameListener pauseGameListener)
            {
                _pauseGameListeners.Remove(pauseGameListener);
            }
            
            if (obj is IResumeGameListener resumeGameListener)
            {
                _resumeGameListeners.Remove(resumeGameListener);
            }
        }

        private void GameTick()
        {
            //Debug.Log($"[{this.GetType().Name}]:" + nameof(GameTick));
            
            if (_gameLoopStatus == GameLoopStatus.GamePlay)
            {
                float deltaTime = Time.deltaTime;
                for (int i = 0; i < _tickGameListeners.Count; i++)
                {
                    _tickGameListeners[i].GameTick(deltaTime);
                }
            }
        }

        void ITickable.Tick()
        {
            GameTick();
        }

        void IInitializable.Initialize()
        {
            Debug.Log($"[{this.GetType().Name}]:" + nameof(IInitializable.Initialize));
            InitGame();
        }

        public void Dispose()
        {
            DeInitGame();
        }
    }
}