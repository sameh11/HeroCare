using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BusinessApp.Infrastructure
{
    public partial class HeroCareCoreContext : IdentityDbContext<User>
    {
        public HeroCareCoreContext(DbContextOptions<HeroCareCoreContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            base.OnModelCreating(modelBuilder);
        }
    }
}
