namespace Bizca.Bff.Infrastructure.Persistence
{
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Repository;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class FolderRepository : BaseRepository<Folder>, IFolderRepository
    {
        public FolderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Folder> GetFolderAsync(int partnerId)
        {
            var partnerIdColumn = GetColumnAttributeName(nameof(Folder.PartnerId));
            IEnumerable<Folder> result = await FindAsync(statement =>
            {
                if (UnitOfWork.Transaction != null) statement.AttachToTransaction(UnitOfWork.Transaction);

                statement
                    .Where($"{partnerIdColumn} = @partnerId")
                    .WithParameters(new { partnerId });
            });

            return result?.FirstOrDefault();
        }
    }
}