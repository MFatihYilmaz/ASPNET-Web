using Microsoft.EntityFrameworkCore;
using WebProgramlama.Models;

namespace WebProgramlama.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }

}