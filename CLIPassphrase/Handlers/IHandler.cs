using CLIPassphrase.Commands;

namespace CLIPassphrase.Handlers;
public interface IHandler<T> where T : ICommand
{
    void Handle(T command);
}