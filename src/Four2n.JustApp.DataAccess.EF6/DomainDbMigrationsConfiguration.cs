using System;
using System.Data.Entity.Migrations;
using System.Linq;

using Four2n.JustApp.Domain.Applications;
using Four2n.JustApp.Domain.Organizational;

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

        protected override void Seed(DomainDbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            CreateWorld(context);
            CreateApplications(context);
            CreateApplicationRentals(context);
            context.Configuration.AutoDetectChangesEnabled = true;
        }

        private void CreateWorld(DomainDbContext context)
        {
            var world = new OrganizationalUnit("World");
            var europe = new OrganizationalUnit("Europe", world);

            var denmark = new OrganizationalUnit("Denmark", europe);
            var hovedstaden = new OrganizationalUnit("Hovedstaden", denmark);
            var copenhagen = new OrganizationalUnit("Copenhagen", hovedstaden);
            var syddanmark = new OrganizationalUnit("Syddanmark", denmark);
            var odense = new OrganizationalUnit("Odense", syddanmark);

            var poland = new OrganizationalUnit("Poland", europe);
            var mazowieckie = new OrganizationalUnit("mazowieckie", poland);
            var warsaw = new OrganizationalUnit("Warsaw", mazowieckie);
            var malopolskie = new OrganizationalUnit("malopolskie", poland);
            var cracow = new OrganizationalUnit("Cracow", malopolskie);

            var africa = new OrganizationalUnit("Africa", world);
            var egipt = new OrganizationalUnit("Egipt", africa);
            var southAfrica = new OrganizationalUnit("South Africa", africa);

            context.OrganizationalUnits.AddRange(
                new[]
                    {
                        world, europe, denmark, hovedstaden, copenhagen, syddanmark, odense, poland,
                        mazowieckie, warsaw, malopolskie, cracow, africa, egipt, southAfrica
                    });
        }

        private void CreateApplications(DomainDbContext context)
        {
            context.Applications.AddRange(new[] {
                new Application("Excel"),
                new Application("GMail"),
                new Application("Word"),
                new Application("Facebook"),
                new Application("GitHub"),
                new Application("Powerpoint")
            });
        }

        private void CreateApplicationRentals(DomainDbContext context)
        {
            context.ApplicationRentals.AddRange(
                new[]
                    {
                        new ApplicationRental(
                            context.Applications.Local.First(x => x.Name == "GMail"),
                            context.OrganizationalUnits.Local.First(x => x.Name == "World"),
                            true),
                        new ApplicationRental(
                            context.Applications.Local.First(x => x.Name == "Facebook"),
                            context.OrganizationalUnits.Local.First(x => x.Name == "World"),
                            true),
                        new ApplicationRental(
                            context.Applications.Local.First(x => x.Name == "Facebook"),
                            context.OrganizationalUnits.Local.First(x => x.Name == "Africa"),
                            true) { Begins = DateTime.Today.Date.AddYears(1) },
                    });
        }
    }
}