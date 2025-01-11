using System;
using Installer;
using UnityEngine;

namespace GameLoop
{
    // класс стал не нужным
    public abstract class ServiceMono<T> : MonoBehaviour, IRegistry<T> where T : class
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
        }

        public void UnRegistry()
        {

        }
    }


    // класс для регистрации объекта в сервис локатор + GameListener 
}