using System;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Input
{
    public sealed class InputManager : ITickGameListener, IInitializable, IDisposable
    {
        private MainGameLoop _mainGameLoop;
        
        
        private Vector2 _direct;
        public Vector2 Direct
        {
            get => _direct;
            set
            {
                _direct = value;
                OnDirectionChanged?.Invoke(value);
            }
        }

        public UnityAction OnShoot;
        public UnityAction<Vector2> OnDirectionChanged;

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }
        
        public InputManager()
        {
            Debug.Log("InputManager Created");
        }

        void ITickGameListener.GameTick(float deltaTime)
        {
            if (UnityEngine.Input.GetKey(KeyCode.Space))
            {
                OnShoot?.Invoke();
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                this._direct.x = -1;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                this._direct.x = 1;
            }
            else
            {
                this._direct.x = 0;
            }
            
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                this._direct.y = 1;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                this._direct.y = -1;
            }
            else
            {
                this._direct.y = 0;
            }
            
            OnDirectionChanged?.Invoke(_direct);
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
    }
}