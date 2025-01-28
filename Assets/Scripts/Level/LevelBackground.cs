using System;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;
using Zenject;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour, IInitializable, IDisposable, IStartPlayGameListener, ITickGameListener
    {
        [Serializable]
        public sealed class Params
        {
            public float m_startPositionY;
            public float m_endPositionY;
            public float m_movingSpeedY;
        }

        [SerializeField] private Params m_params;

        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;
        private Transform myTransform;

        private MainGameLoop _mainGameLoop;

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }

        public void GameTick(float deltaTime)
        {
            if (myTransform.position.y <= this.endPositionY)
            {
                myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

            myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * deltaTime,
                positionZ
            );
        }

        public void StartPlay()
        {
            startPositionY = m_params.m_startPositionY;
            endPositionY = m_params.m_endPositionY;
            movingSpeedY = m_params.m_movingSpeedY;
            myTransform = transform;
            var position = myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        void IInitializable.Initialize()
        {
            _mainGameLoop.Add(this);
        }

        void IDisposable.Dispose()
        {
            _mainGameLoop.Add(this);
        }
    }
}