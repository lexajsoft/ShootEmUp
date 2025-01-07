using System;
using Installer;
using UnityEngine;

namespace GameLoop
{
    public abstract class ServiceMono<T> : MonoBehaviour,IRegistry<T> where T : class
    {
        [SerializeField] private bool _isAutoRegistryOnAwake;
        private bool _isRegistred = false;
        
        protected virtual void Awake()
        {
            if (_isAutoRegistryOnAwake)
            {
                Registry();
            }
        }

        protected virtual void OnDestroy()
        {
            if (_isAutoRegistryOnAwake)
            {
                UnRegistry();
            }
        }

        public void Registry()
        {
            if(_isRegistred)
                return;
            
            if (ServiceLocator.Registy<T>(this))
            {
#if UNITY_EDITOR
                Debug.Log($"Service registred:{typeof(T).Name}");
#endif
                _isRegistred = true;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Service NO registred:{typeof(T).Name}");
#endif
                _isRegistred = false;
            }
        }

        public void UnRegistry()
        {
            if (ServiceLocator.Get<T>() == this)
            {
                ServiceLocator.UnRegistry<T>();
#if UNITY_EDITOR
                Debug.Log($"Service UnRegistred:{typeof(T).Name}");
#endif
                _isRegistred = false;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Service NOT Found/Registred other object:{typeof(T).Name}");
#endif
                _isRegistred = false;
            }
        }
    }


    // класс для регистрации объекта в сервис локатор + GameListener 
}