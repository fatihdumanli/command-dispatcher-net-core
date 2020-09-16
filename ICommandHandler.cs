namespace CommandDistpaching
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void Handle(TCommand command);
    }
}