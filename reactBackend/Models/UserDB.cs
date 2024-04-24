using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace reactBackend.Models
{
    public class UserDB : DbContext
    {

        public UserDB(DbContextOptions<UserDB> options) : base(options)
        {
        }

        public DbSet<UserDB> Users { get; set; }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
