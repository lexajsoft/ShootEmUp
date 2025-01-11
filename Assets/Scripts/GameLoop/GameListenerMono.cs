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
            Debug.Log("I Want Registry:" + this.GetType().Name);
            IGameListener.OnRegistry?.Invoke(this);
            OnStart();
        }

        protected void OnDestroy()
        {
            Debug.Log("I Want Destroy self:" + this.GetType().Name);
            IGameListener.OnUnRegistry?.Invoke(this);
        }
    }
}
