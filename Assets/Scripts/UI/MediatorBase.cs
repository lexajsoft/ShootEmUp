using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(ViewBase))]
    public abstract class MediatorBase<T> : MonoBehaviour where T : ViewBase
    {
        [SerializeField] protected T _viewBase;
        public abstract void Notify();
        public abstract void Subscribes();
        public abstract void Describes();

        private void OnEnable()
        {
            Enable();
            Subscribes();
            
        }

        private void OnDisable()
        {
            Disable();
            Describes();
        }

        protected abstract void Disable();
        protected abstract void Enable();

    }
}