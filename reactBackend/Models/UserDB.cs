using Microsoft.EntityFrameworkCore;

namespace reactBackend.Models
{
    public class UserDB : DbContext
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }



}
