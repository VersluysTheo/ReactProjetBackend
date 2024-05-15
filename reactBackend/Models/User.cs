using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace reactBackend.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int BienId { get; set; }
        public Bien Bien { get; set; }
    }



    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bien> Biens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Bien)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BienId);
        }
    }
}
