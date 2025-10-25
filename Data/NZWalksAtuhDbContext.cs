using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalksAPI.Data
{
    public class NZWalksAtuhDbContext : IdentityDbContext
    {
        public NZWalksAtuhDbContext(DbContextOptions<NZWalksAtuhDbContext> options) : base(options)
        {

          
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReadRoleId = "5f8e170e-8186-4f54-8cbf-ca56503075fa";
            var WriterRoleId = "41f7fc5e-0d33-4c40-8c31-04ada5b39933";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReadRoleId,
                    ConcurrencyStamp = ReadRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader"

                },
                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp= WriterRoleId,
                    Name = "Writer",
                    NormalizedName ="Writer".ToUpper()

                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
