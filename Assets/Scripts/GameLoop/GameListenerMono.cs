using GameLoop.Interfaces;
using UnityEngine;

namespace GameLoop
{
    public abstract class GameListenerMono : MonoBehaviour, IGameListener
    {
        protected virtual void OnStart()
        {
            
        }

        protected void Start()
        {
            IGameListener.OnRegistry?.Invoke(this);
            OnStart();
        }

        protected void OnDestroy()
        {
            IGameListener.OnUnRegistry?.Invoke(this);
        }
    }
}