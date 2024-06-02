using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WalkDifficulty> CreateAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();

            await dbContext.WalkDifficulty.AddAsync(walkDifficulty);

            await dbContext.SaveChangesAsync();

            return walkDifficulty;

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if(walkDifficulty == null)
            {
                return null;
            }

            dbContext.WalkDifficulty.Remove(walkDifficulty);
            await dbContext.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await dbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetByIdAsync(Guid id)
        {
            return await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await dbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
