using System;

using Four2n.JustApp.Domain.Organizational;

using Microsoft.Data.Entity;

namespace Four2n.JustApp.DataAccess.EF
{
    public class DomainDbContext : DbContext
    {
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }

        public DomainDbContext()
            : base()
        {
            throw new NotSupportedException("I dont wanna be here ... yet");
        }

        public DomainDbContext(string connectionString)
        {
            throw new NotImplementedException();
        }

        public static void Initialize(string connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
