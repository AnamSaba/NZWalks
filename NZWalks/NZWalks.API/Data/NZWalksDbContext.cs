using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for walkdifficulty
            //Easy, Medium, Hard

            var difficulties = new List<WalkDifficulty>()
            {
                new WalkDifficulty()
                {
                    Id = Guid.Parse("fe140afa-4217-46e0-b65f-7e76f64ee2e3"),
                    Code = "Easy"
                },
                new WalkDifficulty()
                {
                    Id = Guid.Parse("07a52212-552c-4e30-8000-d118a78e5ac7"),
                    Code = "Medium"
                },
                new WalkDifficulty()
                {
                    Id = Guid.Parse("a4cfcd9e-ccf8-4f3f-82ea-4f37c2de3368"),
                    Code = "Hard"
                }
            };

            //Seed WalkDifficulties to database
            modelBuilder.Entity<WalkDifficulty>().HasData(difficulties);


            // Seed data for Regions

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("7391432f-c164-4e3e-9873-b2c57389d773"),
                    Code = "AKL",
                    Name = "Auckland",
                    Area = 4894,
                    Lat = -36.5253207,
                    Long = 173.7785704,
                    Population = 1718982,
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("79e9872d-5a2f-413e-ac36-511036ccd3d4"),
                    Code = "WAIK",
                    Name = "Waikato",
                    Area = 8970,
                    Lat = -37.5144584,
                    Long = 174.5405128,
                    Population = 496700,
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("68c2ab66-c5eb-40b6-81e0-954456d06bba"),
                    Code = "BAYP",
                    Name = "Bay Of Plenty",
                    Area = 12230,
                    Lat = -37.5328259,
                    Long = 175.7642701,
                    Population = 345400,
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Code = "WGN",
                    Name = "Wellington",
                    Area = 5230,
                    Lat = -37.5316259,
                    Long = 178.7642341,
                    Population = 134400,
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },

            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
