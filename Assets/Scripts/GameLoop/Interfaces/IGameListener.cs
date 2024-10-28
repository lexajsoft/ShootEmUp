using System;

namespace GameLoop.Interfaces
{
    public interface IGameListener
    {
        static Action<Object> OnRegistry;
        static Action<Object> OnUnRegistry;
    }
}