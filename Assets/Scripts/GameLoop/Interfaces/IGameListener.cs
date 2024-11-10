using System;

namespace GameLoop.Interfaces
{
    public interface IGameListener
    {
        static Action<IGameListener> OnRegistry;
        static Action<IGameListener> OnUnRegistry;
    }
}