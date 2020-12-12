namespace Bizca.Core.Infrastructure.Test
{
    using Bizca.Core.Domain.Repositories;

    public class UnitOfWorkRepository : IRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public UnitOfWorkRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
