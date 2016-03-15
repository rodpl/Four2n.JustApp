using System.Data.Entity;

using Four2n.JustApp.Domain.Applications;
using Four2n.JustApp.Domain.Organizational;

namespace Four2n.JustApp.DataAccess.EF
{
    public class DomainDbContext : DbContext
    {
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationRental> ApplicationRentals { get; set; }

        public DomainDbContext()
            : base("DefaultConnection")
        {
        }

        public DomainDbContext(string connectionString): base(connectionString)
        {
        }

        public static void Initialize(string connectionString)
        {
            using (var ctx = new DomainDbContext(connectionString))
            {
                new DomainMigrationDataContextInitializer().InitializeDatabase(ctx);
            }
        }
    }
}
