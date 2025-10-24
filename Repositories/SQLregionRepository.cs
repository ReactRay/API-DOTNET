using Microsoft.EntityFrameworkCore;
using NZwalksAPI.Data;
using NZwalksAPI.Models.Domain;

namespace NZwalksAPI.Repositories
{
    public class SQLregionRepository : IRegionRepository
    {
        private readonly NZwalksDbContext dbContext;
        public SQLregionRepository(NZwalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //functions

        public async  Task<Region?> CreateRegionAsync(Region region)
        {
           await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

      

        public async  Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

     

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

    

        public async  Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var exists = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(exists == null)
            {
                return null;
            }
            exists.Name = region.Name;
            exists.Code = region.Code;
            exists.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return exists;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var exists = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exists == null) return null;

            dbContext.Regions.Remove(exists);
            await dbContext.SaveChangesAsync();
            return exists;
        }
    }
}
