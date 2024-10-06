using System;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;

        private Vector2 _direct;
        
        
        public void SetDirectToMove(Vector2 vector)
        {
            _direct = vector;
            // var nextPosition = this._rigidbody2D.position + vector * this._speed;
            // this._rigidbody2D.MovePosition(nextPosition);
        }

        private void FixedUpdate()
        {
            var nextPosition = this._rigidbody2D.position + _direct * Time.deltaTime * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}