namespace Bizca.Bff.Infrastructure.Persistance
{
    using Core.Infrastructure;
    using Domain.Provider.Folder;
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