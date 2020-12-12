namespace Bizca.Core.Application.Test.Cqrs
{
    using Bizca.Core.Application.Abstracts.Commands;

    public class FakeCommand : ICommand<FakeResponse>
    {
    }

    public class FakeCommand2 : ICommand
    {
    }
}
