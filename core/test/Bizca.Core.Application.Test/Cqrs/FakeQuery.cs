namespace Bizca.Core.Application.Test.Cqrs
{
    using Bizca.Core.Application.Queries;
    public class FakeQuery2 : IQuery
    {
    }

    public class FakeQuery : IQuery<FakeResponse>
    {
    }
}