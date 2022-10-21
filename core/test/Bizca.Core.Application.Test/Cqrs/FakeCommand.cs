namespace Bizca.Core.Application.Test.Cqrs
{
    using Domain.Cqrs.Commands;

    public class FakeCommand : ICommand<FakeResponse>
    {
    }

    public class FakeCommand2 : ICommand
    {
    }
}