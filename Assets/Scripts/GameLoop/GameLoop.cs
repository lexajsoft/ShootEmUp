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
        None,
        GameStart,
        GamePlay,
        GamePause,
        GameResume,
        GameStop
    }

    public class GameLoop : MonoBehaviour, IRegistry
    {
        [SerializeField] private GameLoopStatus _gameLoopStatus = GameLoopStatus.None;
        
        private Dictionary<Type, List<object>> _objects;

        private void Awake()
        {
            _objects = new Dictionary<Type, List<object>>();
            
            _objects.Add(typeof(IGameListener),new List<object>());
            _objects.Add(typeof(IGameListenerStart),new List<object>());
            _objects.Add(typeof(IGameListenerStop),new List<object>());
            _objects.Add(typeof(IGameListenerTick),new List<object>());
            _objects.Add(typeof(IGameListenerPause),new List<object>());
            _objects.Add(typeof(IGameListenerResume),new List<object>());
            _objects.Add(typeof(IGameListenerNone),new List<object>());

            IGameListener.OnRegistry += OnGameListenerOnRegistry;
            IGameListener.OnUnRegistry += OnGameListenerUnOnRegistry;
        }

        private IEnumerator Start()
        {
            yield return null;
            new SetStatusGameLoopCommand(GameLoopStatus.None).Execute();
        }

        private void OnGameListenerOnRegistry(object obj) => Registry(obj as IGameListener);
        private void OnGameListenerUnOnRegistry(object obj) => Registry(obj as IGameListener);
        
        private void OnDestroy()
        {
            _objects.Clear();
            
            IGameListener.OnRegistry -= OnGameListenerOnRegistry;
            IGameListener.OnUnRegistry -= OnGameListenerUnOnRegistry;
        }

        public void Registry(IGameListener obj)
        {
            if (obj is not null)
            {
                Add<IGameListener>(obj);
                Add<IGameListenerStart>(obj);
                Add<IGameListenerStop>(obj);
                Add<IGameListenerTick>(obj);
                Add<IGameListenerPause>(obj);
                Add<IGameListenerResume>(obj);
                Add<IGameListenerNone>(obj);
            }
        }
        
        public void UnRegistry(IGameListener obj)
        {
            if (obj is not null)
            {
                Remove<IGameListener>(obj);
                Remove<IGameListenerStart>(obj);
                Remove<IGameListenerStop>(obj);
                Remove<IGameListenerTick>(obj);
                Remove<IGameListenerPause>(obj);
                Remove<IGameListenerResume>(obj);
                Remove<IGameListenerNone>(obj);
            }
        }

        public void SetStatus(GameLoopStatus gameLoopStatus)
        {
            if(_gameLoopStatus == gameLoopStatus)
                return;
            
            Debug.Log(_gameLoopStatus.ToString() +  "=>" + gameLoopStatus.ToString());
            
            _gameLoopStatus = gameLoopStatus;
            switch (_gameLoopStatus)
            {
                case GameLoopStatus.None:
                {
                    // var list = _objects[typeof(IGameListenerNone)].Select(item => item as IGameListenerNone).ToList();
                    // for (int i = 0; i < list.Count; i++)
                    // {
                    //     list[i].GameNone();
                    // }

                    break;
                }
                case GameLoopStatus.GameStart:
                {
                    var list = _objects[typeof(IGameListenerStart)].Select(item => item as IGameListenerStart).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].GameStart();
                    }

                    SetStatus(GameLoopStatus.GamePlay);
                    break;
                }
                case GameLoopStatus.GamePlay:
                {
                    break;
                }
                case GameLoopStatus.GamePause:
                {
                    var list = _objects[typeof(IGameListenerPause)].Select(item => item as IGameListenerPause).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].GamePause();
                    }

                    break;
                }
                case GameLoopStatus.GameResume:
                {
                    var list = _objects[typeof(IGameListenerResume)].Select(item => item as IGameListenerResume)
                        .ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].GameResume();
                    }

                    SetStatus(GameLoopStatus.GamePlay);
                    break;
                }
                case GameLoopStatus.GameStop:
                {
                    var list = _objects[typeof(IGameListenerStop)].Select(item => item as IGameListenerStop).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].GameStop();
                    }

                    break;
                }
            }
        }

        private void Add<T>(object obj) where T : IGameListener
        {
            if (obj is T)
            {
                var type = typeof(T);
                _objects[type].Add(obj);
            }
        }
        
        private void Remove<T>(object obj) where T : IGameListener
        {
            if (obj is T)
            {
                var type = typeof(T);
                _objects[type].Remove(obj);
            }
        }

        private void Update()
        {
            if (_gameLoopStatus == GameLoopStatus.GamePlay)
            {
                var list = _objects[typeof(IGameListenerTick)].Select(item => item as IGameListenerTick).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].GameTick(Time.deltaTime);
                }         
            }
        }

        public void Registry()
        {
            ServiceLocator.Registy(typeof(GameLoop), this);
        }
    }
}