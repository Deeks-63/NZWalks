using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        //Get the single region
        Task<Region> GetAsync(Guid Id);

        Task<Region> AddSync(Region region);
        Task<Region> DeleteAsync(Guid Id);
        Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
