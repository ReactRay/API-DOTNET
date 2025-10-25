using NZwalksAPI.Models.Domain;
using NZwalksAPI.Models.DTO;

namespace NZwalksAPI.Repositories
{
    public interface IWalkRepository
    {

        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync
       (string? filterOn = null, string? filterQuery = null,
       string? sortBy = null, bool isAscending = true,int pageNumber=1,int PageSize =5);
         
        Task <Walk?> GetWalkByIdAsync(Guid id);

        Task<Walk?> updateWalkAsync(Guid id, Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
