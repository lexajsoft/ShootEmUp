using System;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : GameListenerMono, ITickGameListener
    {
        [SerializeField] private float _speed = 5.0f;

        private Vector2 _direct;
        
        
        public void SetDirectToMove(Vector2 vector)
        {
            _direct = vector;
        }

        public void GameTick(float deltaTime)
        {
            transform.position += (Vector3)_direct * (deltaTime * _speed);
        }
    }
}