using Components;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : GameListenerMono, ITickGameListener
    {
        [SerializeField] private MoveComponent moveComponent;
        private Vector2 destination;
        private bool isReached;
        
        public bool IsReached
        {
            get { return isReached; }
        }



        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            isReached = false;
        }

        public void GameTick(float deltaTime)
        {
            if (isReached)
            {
                moveComponent.SetDirectToMove(Vector2.zero);
                return;
            }
            
            var vector = destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized;
            moveComponent.SetDirectToMove(direction);
        }
    }
}