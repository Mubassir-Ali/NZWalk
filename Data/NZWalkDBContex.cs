using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDBContex : DbContext
    {
        public NZWalkDBContex(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("dda1daea-734c-4dd1-ac3c-1c9e4d168dac"),
                    Name="Easy"
                },

                new Difficulty()
                {
                    Id=Guid.Parse("e4549b08-11e3-4385-8163-0fccd6459bcc"),
                    Name="Medium"
                },

                new Difficulty()
                {
                    Id=Guid.Parse("0fbd1bc7-f3ab-4914-a92e-b0fff04a5c24"),
                    Name="Hard"
                }
            };

            // Seed Data to difficulties database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Region
            var region = new List<Region>
            {
                new Region()
                {
                    Id=Guid.Parse("c7c9a318-1ab0-42f3-bd28-52187f9b5be7"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageUrl="ak-image.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("e281df03-351d-470e-8304-1c7d2026fa50"),
                    Name="Northland",
                    Code="NTL",
                    RegionImageUrl="ntl-image.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("965ff222-0543-41a9-883f-c79ed940191a"),
                    Name="Bay of Planty",
                    Code="BOP",
                    RegionImageUrl="bop-image.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("d40fa755-7d03-45b4-a516-2b869b7ec4f9"),
                    Name="Welligton",
                    Code="WGN",
                    RegionImageUrl="wgn-image.jpg"
                },

                new Region()
                {
                    Id=Guid.Parse("024d447e-06bc-4ec2-a383-497288dc3ee3"),
                    Name="Nelson",
                    Code="NSL",
                    RegionImageUrl="nsl-image.jpg"
                },

                new Region()
                {
                    Id=Guid.Parse("5ebfec4a-a5c5-4264-b910-b21f82ab3ac9"),
                    Name="Southland",
                    Code="STL",
                    RegionImageUrl="stl-image.jpg"
                },
            };

            // Seed data to Region database
            modelBuilder.Entity<Region>().HasData(region);


        }
    }
}
