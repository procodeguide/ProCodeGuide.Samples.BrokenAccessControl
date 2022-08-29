using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;

namespace ProCodeGuide.Samples.BrokenAccessControl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "5736062e-cf47-4323-9de9-ffbba4161963", Name = "Administrator", ConcurrencyStamp = "1", NormalizedName = "Administrator" },
                new IdentityRole() { Id = "720e8e4d-1b1e-4c80-8448-841bfd0a1355", Name = "Standard User", ConcurrencyStamp = "2", NormalizedName = "Standard User" }
                );
        }

        public DbSet<PostEntity> Posts { get; set; }
    }
}