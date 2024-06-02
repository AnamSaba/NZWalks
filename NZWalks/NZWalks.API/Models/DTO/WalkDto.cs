using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }



        //Navigation Property
        public WalkDifficultyDto WalkDifficulty { get; set; }

        public RegionDto Region { get; set; }
    }
}
