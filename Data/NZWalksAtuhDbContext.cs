using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalksAPI.Data
{
    public class NZWalksAtuhDbContext : IdentityDbContext
    {
        public NZWalksAtuhDbContext(DbContextOptions<NZWalksAtuhDbContext> options) : base(options)
        {
        }
    }
}
