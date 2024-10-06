using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public sealed class InputManager : MonoBehaviour
    {
        private Vector2 _direct;

        public Vector2 Direct
        {
            get => _direct;
            set
            {
                _direct = value;
                OnHorizontalDirectionChanged?.Invoke(value);
            }
        }

        public UnityAction OnShoot;
        public UnityAction<Vector2> OnHorizontalDirectionChanged;

        private void Update()
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
            
            OnHorizontalDirectionChanged?.Invoke(_direct);
        }
    }
}