using Microsoft.EntityFrameworkCore;
using TranslatorAPI.Models;

namespace TranslatorAPI.Data
{
   public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
