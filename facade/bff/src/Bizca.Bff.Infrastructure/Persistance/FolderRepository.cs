namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure;
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
            var result = await FindAsync(statement =>
            {
                if (UnitOfWork.Transaction != null)
                {
                    statement.AttachToTransaction(UnitOfWork.Transaction);
                }

                statement
                    .Where($"{partnerIdColumn} = @partnerId")
                    .WithParameters(new { partnerId });
            });

            return result?.FirstOrDefault();
        }
    }
}
