using System;
using Autofac;
using Microsoft.Extensions.Logging;

namespace CommandDistpaching
{
    public class CommandDispatcher
    {
        private ILogger<CommandDispatcher> _logger;
        private ILifetimeScope _scope;
        public CommandDispatcher(ILifetimeScope scope, ILogger<CommandDispatcher> logger = null)
        {
           _logger = logger;
           _scope = scope;
           
           _logger.LogInformation(" [x] CommandDispatcher: Creating an instance of CommandDispatcher.");
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand: Command
        {
            using(var currentScope = _scope.BeginLifetimeScope())
            {
                try
                {
                    _logger.LogInformation(string.Format(" [x] CommandDispatcher.Dispatch(): Dispatching the command {0}", command.GetType().Name));
                    var handler = currentScope.Resolve<ICommandHandler<TCommand>>();
                    _logger.LogInformation(string.Format(" [x] CommandDispatcher.Dispatch(): Resolved an instance of {0}", handler.GetType().Name));
                    handler.Handle(command);
                } 
                
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
               
            }
        }
    }
}