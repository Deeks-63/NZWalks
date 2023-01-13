using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddSync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region; 
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if(region == null)
            {
                return null;
            }
            //Delete the region
            nZWalksDbContext.Regions.Remove(region);

            //To save the changes
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        //Made this function asynchronous after making IREgionRepository TASK
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
           var existingRegion =  await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code= region.Code;
            existingRegion.Name= region.Name;
            existingRegion.Lat= region.Lat;
            existingRegion.Long= region.Long;  
            existingRegion.Population = region.Population;
            existingRegion.Area= region.Area;

            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
