namespace Bizca.Core.Application.Test.Cqrs
{
    using Queries;

    public class FakeQuery2 : IQuery
    {
    }

    public class FakeQuery : IQuery<FakeResponse>
    {
    }
}