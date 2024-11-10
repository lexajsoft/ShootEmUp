using System;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : GameListenerMono, IStartPlayGameListener ,ITickGameListener
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



        
        

        public void GameTick(float deltaTime)
        {
            if (this.myTransform.position.y <= this.endPositionY)
            {
                this.myTransform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.myTransform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * deltaTime,
                this.positionZ
            );
        }

        public void StartPlay()
        {
            this.startPositionY = this.m_params.m_startPositionY;
            this.endPositionY = this.m_params.m_endPositionY;
            this.movingSpeedY = this.m_params.m_movingSpeedY;
            this.myTransform = this.transform;
            var position = this.myTransform.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }
    }
}