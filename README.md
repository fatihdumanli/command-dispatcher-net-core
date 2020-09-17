
# domain-dispatcher-net-core
The simplest implementation of Mediator pattern in .NET core.

# Usage

## Commanding

### 1) Define Command objects
Define domain command objects by inheriting from Command class (DomainDispatching.Commanding.Command).

### 2) Define CommandHandler class
Define a handler class for each command to handle commands by inheriting from ICommandHandler interface.

### 3) Register your artifacts to Autofac container.

    builder.RegisterType<CreateOrderCommandHandler>().As<ICommandHandler<CreateOrderCommand>>();

### 4) Add DomainDispatcher to your ServiceProvider

    services.AddSingleton<DomainDispatcher>();
