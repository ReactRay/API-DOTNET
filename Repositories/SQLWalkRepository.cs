using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZwalksAPI.Data;
using NZwalksAPI.Models.Domain;
using NZwalksAPI.Models.DTO;

namespace NZwalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZwalksDbContext dbContext;
        private readonly IMapper mapper;

        public SQLWalkRepository(NZwalksDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async  Task<Walk> CreateWalkAsync(Walk walk)
        {

            await dbContext.AddAsync(walk);

            await dbContext.SaveChangesAsync();

            return walk;
           
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);  

            if (walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllWalksAsync
        (string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
         int pageNumber = 1,int pageSize =5)
        {
            var Walks =  dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
                }
            }

            //sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.Name) : Walks.OrderByDescending(x => x.Name); 
                }
                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.LengthInKm) : Walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            int steps = (pageNumber - 1) * pageSize;

            Walks = Walks.Skip(steps).Take(steps);

            return await Walks.ToListAsync();
        }
        // get one walk

        public async  Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var Walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            return Walk;
        }

        public async  Task<Walk?> updateWalkAsync(Guid id, Walk walk)
        {
           Walk exists = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(exists == null)
            {
                return null;
            }

            exists.Name = walk.Name;
            exists.Description = walk.Description;
            exists.DifficultyId = walk.DifficultyId;
            exists.RegionId = walk.RegionId;
            exists.WalkImageUrl = walk.WalkImageUrl;
            exists.LengthInKm = walk.LengthInKm;

            await dbContext.SaveChangesAsync();

            return exists;
        }
    }
}
