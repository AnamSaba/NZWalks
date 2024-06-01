using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public RegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            
            await dbContext.AddAsync(region);

            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingDomain == null) 
            {
                return null;
            }

            existingDomain.Code = region.Code;
            existingDomain.Name = region.Name;
            existingDomain.Area = region.Area;
            existingDomain.Lat  = region.Lat;
            existingDomain.Long = region.Long;
            existingDomain.Population = region.Population;
            existingDomain.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existingDomain;

        }
    }
}
