using System;
using Autofac;
using DomainDispatching.Commanding;
using DomainDispatching.DomainEvent;
using Microsoft.Extensions.Logging;

namespace DomainDispatching
{
    public class DomainDispatcher
    {   
        private ILogger<DomainDispatcher> _logger;
        private ILifetimeScope _scope;
        public DomainDispatcher(ILifetimeScope scope, ILogger<DomainDispatcher> logger = null)
        {
           _logger = logger;
           _scope = scope;
           
           _logger.LogInformation(" [x] DomainDispatcher: Creating an instance of DomainDispatcher.");
        }

        public void DispatchCommand<TCommand>(TCommand command) where TCommand: Command
        {
            using(var currentScope = _scope.BeginLifetimeScope())
            {
                try
                {
                    _logger.LogInformation(string.Format(" [x] DomainDispatcher.DispatchCommand(): Dispatching the command {0}", command.GetType().Name));
                    var handler = currentScope.Resolve<ICommandHandler<TCommand>>();
                    _logger.LogInformation(string.Format(" [x] DomainDispatcher.DispatchCommand(): Resolved an instance of {0}", handler.GetType().Name));
                    handler.Handle(command);
                } 
                
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
               
            }
        }
    
        public void PublishDomainEvent<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            using(var currentScope = _scope.BeginLifetimeScope())
            {
                try
                {
                    _logger.LogInformation(string.Format(" [x] DomainDispatcher.PublishDomainEvent(): Publishing the domain event: {0}", @event.GetType().Name));
                    var domainEventHandler = currentScope.Resolve<IDomainEventHandler<TDomainEvent>>();
                    _logger.LogInformation(string.Format(" [x] DomainDispatcher.PublishDomainEvent(): Resolved an instance of {0}", domainEventHandler.GetType().Name));
                    domainEventHandler.Handle(@event);
                }

                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }           
            }
        }
    }
}