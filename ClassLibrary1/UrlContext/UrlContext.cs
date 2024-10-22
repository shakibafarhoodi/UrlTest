using Domin.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.PnsContext
{
    public class UrlContext : IdentityDbContext
    {
        public UrlContext(DbContextOptions<UrlContext> option) : base(option)
        {
        }
        public DbSet<UrlModel> Url { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
