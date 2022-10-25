using Microsoft.EntityFrameworkCore;

namespace CelupartsPoC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserDto> UsersDto { get; set; }
        public DbSet<RequestWithEquipments> Request { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Repair> Repair { get; set; }
        public DbSet<Technician> Technician { get; set; }
        public DbSet<RepairPayment> RepairPayment { get; set; }
        public DbSet<Courier> Courier { get; set; }
        public DbSet<HomeService> HomeService { get; set;  }
        public DbSet<Retoma> Retoma { get; set; }
        public DbSet<RetomaPayment> RetomaPayment { get; set; }
        public DbSet<RequestNotification> RequestNotification { get; set; }
        public DbSet<CelupartsInfo> CelupartsInfo { get; set; }
        public DbSet<TypeOfEquipment> TypeOfEquipment { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<RequestHistory> RequestHistory { get; set; }
    }
}
