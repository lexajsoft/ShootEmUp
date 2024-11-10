using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using GameLoop.Interfaces;
using Installer;
using UnityEngine;

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

    [DefaultExecutionOrder(-1000)]
    public class GameLoop : ServiceMono<GameLoop>
    {
        [SerializeField] private Installer.Installer _installer;
        
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


        protected override void Awake()
        {
            base.Awake();

            _initGameListeners = new List<IInitGameListener>();
            _startPlayGameListeners = new List<IStartPlayGameListener>();
            _resumeGameListeners = new List<IResumeGameListener>();
            _tickGameListeners = new List<ITickGameListener>();
            _pauseGameListeners = new List<IPauseGameListener>();
            _finishGameListeners = new List<IFinishGameListener>();

            IGameListener.OnRegistry += OnGameListenerOnRegistry;
            IGameListener.OnUnRegistry += OnGameListenerUnOnRegistry;
            
            _installer.Install();
        }

        private IEnumerator Start()
        {
            yield return null;
            _gameLoopStatus = GameLoopStatus.GameInit; 
            UpdateStatus();
        }

        private void OnGameListenerOnRegistry(IGameListener obj) => RegistryGameListener(obj);
        private void OnGameListenerUnOnRegistry(IGameListener obj) => RegistryGameListener(obj);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            IGameListener.OnRegistry -= OnGameListenerOnRegistry;
            IGameListener.OnUnRegistry -= OnGameListenerUnOnRegistry;
        }

        public void RegistryGameListener(IGameListener obj)
        {
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
            for (int i = 0; i < _initGameListeners.Count; i++)
            {
                _initGameListeners[i].GameInit();
            }

            //_gameLoopStatus = GameLoopStatus.GamePlay;
        }

        private void GameFinish()
        {
            for (int i = 0; i < _finishGameListeners.Count; i++)
            {
                _finishGameListeners[i].GameFinish();
            }
        }

        private void GameResume()
        {
            for (int i = 0; i < _resumeGameListeners.Count; i++)
            {
                _resumeGameListeners[i].GameResume();
            }

            SetStatus(GameLoopStatus.GamePlay);
            UpdateStatus();
        }

        private void GamePause()
        {
            for (int i = 0; i < _pauseGameListeners.Count; i++)
            {
                _pauseGameListeners[i].GamePause();
            }
        }
        
        private void GamePlay()
        {
            for (int i = 0; i < _startPlayGameListeners.Count; i++)
            {
                _startPlayGameListeners[i].StartPlay();
            }
        }

        private void Add(IGameListener obj)
        {
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
        
        private void Remove(IGameListener obj)
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

        private void Update()
        {
            GameTick();
        }

        private void GameTick()
        {
            if (_gameLoopStatus == GameLoopStatus.GamePlay)
            {
                float deltaTime = Time.deltaTime;
                for (int i = 0; i < _tickGameListeners.Count; i++)
                {
                    _tickGameListeners[i].GameTick(deltaTime);
                }
            }
        }
    }
}