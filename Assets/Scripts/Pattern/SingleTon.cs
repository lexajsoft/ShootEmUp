using UnityEngine;

namespace Pattern
{
    public class SingleTon<T> : MonoBehaviour where T : class
    {
        private static T _instance = null;

        public static T Instance => _instance;

        private void Awake()
        {
            _instance = this as T;
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        
        }
    }
}