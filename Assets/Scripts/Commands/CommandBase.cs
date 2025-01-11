using Zenject;

namespace Commands
{
    public abstract class CommandBase
    {
        public abstract void Execute();
        
        
    }

    public abstract class CommandBase<TClass> : CommandBase
    {
        
    }
    
    public abstract class CommandFactory<TCommand> : IFactory<TCommand> where TCommand : new()
    {
        public TCommand Create()
        {
            return new TCommand();
        }
    }
}