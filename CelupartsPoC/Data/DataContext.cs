using Microsoft.EntityFrameworkCore;

namespace CelupartsPoC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserDto> UsersDto { get; set; }
    }
}
