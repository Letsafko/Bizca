namespace Bizca.Bff.Domain.Provider.Folder
{
    using System.Threading.Tasks;

    public interface IFolderRepository
    {
        Task<Folder> GetFolderAsync(int partnerId);
    }
}