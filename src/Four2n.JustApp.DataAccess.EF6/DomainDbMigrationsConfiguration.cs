using System.Data.Entity.Migrations;

namespace Four2n.JustApp.DataAccess.EF
{
    public class DomainDbMigrationsConfiguration : DbMigrationsConfiguration<DomainDbContext>
    {
        public DomainDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DomainDbContext";
        }
    }
}