using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

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
            var initializer =
                new MigrateDatabaseToLatestVersion<DomainDbContext, DomainDbMigrationsConfiguration>
                    (true);

            Database.SetInitializer(initializer);

            using (var ctx = new DomainDbContext(connectionString))
            {
                ctx.Database.Initialize(false);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadAllEntityTypeConfiguration(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void LoadAllEntityTypeConfiguration(DbModelBuilder modelBuilder)
        {
            //Culture is a class defined in the assembly where my Entity models reside
            var typesToRegister = Assembly.GetAssembly(typeof(DomainDbContext)).GetTypes()
                .Where(type => type.Namespace.EndsWith(".Mapping"));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
