using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalkDBContex dbContex;

        public SQLRegionRepository(NZWalkDBContex dbContex)
        {
            this.dbContex = dbContex;
        }


        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContex.Region.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContex.Region.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContex.Region.AddAsync(region);
            await dbContex.AddAsync(region);

            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContex.Region.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            // Map DTO to Domain Model
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContex.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion=await dbContex.Region.FirstOrDefaultAsync(x=>x.Id==id);
            if(existingRegion== null)
            {
                return null; 
            }

            dbContex.Region.Remove(existingRegion);
            await dbContex.SaveChangesAsync();

            return existingRegion;

            
        }
    }
}
