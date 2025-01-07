namespace Installer
{
    // для предварительной кастомной регистрации
    public interface IRegistry
    {
        public void Registry();
        public void UnRegistry();
    }
    
    public interface IRegistry<T> : IRegistry
    {
        
    }
}