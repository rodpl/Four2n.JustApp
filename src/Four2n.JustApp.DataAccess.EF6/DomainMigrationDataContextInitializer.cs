using System.Data.Entity;

namespace Four2n.JustApp.DataAccess.EF
{
    public class DomainMigrationDataContextInitializer : MigrateDatabaseToLatestVersion<DomainDbContext, DomainDbMigrationsConfiguration>
    {
        public DomainMigrationDataContextInitializer() : base(true)
        {
        }
    }
}