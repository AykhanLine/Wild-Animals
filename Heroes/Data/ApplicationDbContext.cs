using Heroes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<People> Peoples{ get; set; }
        public DbSet<Responsiv> Responsives { get; set; }
        public DbSet<SignUp> SignUps { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
        }

    }
}
