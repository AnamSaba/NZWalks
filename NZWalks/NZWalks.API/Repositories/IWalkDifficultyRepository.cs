using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
       Task<IEnumerable<WalkDifficulty>> GetAllAsync();
       Task<WalkDifficulty> GetByIdAsync(Guid id);
       Task<WalkDifficulty> CreateAsync(WalkDifficulty walkDifficulty);
       Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);
       Task<WalkDifficulty> DeleteAsync(Guid id);


    }
}
