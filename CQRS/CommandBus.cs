using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class CommandBus
    {
        private readonly IServiceProvider _container;

        public CommandBus(IServiceProvider container)
        {
            _container = container;
        }

        public void Handle<T>(T command) where T : ICommand
        {
            ICommandHandler<T> commandHandler = _container.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;
            
            if(commandHandler != null)
            {
                commandHandler.Handle(command);
            }
            else
            {
                throw new Exception("Method " + typeof(T).Name + " is not supported.");
            }

        }
    }
}
