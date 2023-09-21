using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {

        private readonly NZWalkDBContex dBContex;

        public SQLWalkRepository(NZWalkDBContex dBContex)
        {
            this.dBContex = dBContex;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dBContex.Walk.AddAsync(walk);
            await dBContex.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var exitingWalk=await dBContex.Walk.FirstOrDefaultAsync(x=>x.Id==id);
            if(exitingWalk ==null)
            {
                return null;
            }

            dBContex.Walk.Remove(exitingWalk); 
            await dBContex.SaveChangesAsync();

            return exitingWalk;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dBContex.Walk.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await dBContex.Walk.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await dBContex.Walk.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            dBContex.SaveChanges();

            return existingWalk;
        }
    }
}
