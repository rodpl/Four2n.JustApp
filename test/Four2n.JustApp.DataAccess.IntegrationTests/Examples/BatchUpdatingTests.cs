using System.Data.Entity;
using System.Linq;

using Four2n.JustApp.DataAccess.EF;
using Four2n.JustApp.Domain.Applications;
using Four2n.JustApp.Domain.SharedKernel;

using Xunit;
using Xunit.Abstractions;

namespace Four2n.JustApp.DataAccess.IntegrationTests.Examples
{
    public class BatchUpdatingTests : RollbackFixture
    {
        private readonly ITestOutputHelper _output;

        public BatchUpdatingTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        private interface IApplicationRepository
        {
            int ChangeCurrency(
                string fromCurrencyCode,
                string toCurrencyCode,
                decimal conversionRate);
        }

        private class ApplicationRepository : IApplicationRepository
        {
            internal readonly DomainDbContext _dbContext;

            public ApplicationRepository(DomainDbContext dbContext)
            {
                this._dbContext = dbContext;
            }

            public int ChangeCurrency(
                string fromCurrencyCode,
                string toCurrencyCode,
                decimal conversionRate)
            {
                var changedRecords = _dbContext.Database.ExecuteSqlCommand(@"
                    UPDATE Applications
                    SET BasePrice_Currency = @p1
                    , BasePrice_Amount = BasePrice_Amount * @p2
                    WHERE BasePrice_Currency = @p0", fromCurrencyCode, toCurrencyCode, conversionRate);
                return changedRecords;
            }
        }

        [Fact]
        public void Batch_updating_needs_entities_detaching()
        {
            using (var ctx = CreateDbContext(_output))
            {
                // Arrange
                ctx.Applications.AddRange(
                    new[]
                        {
                            new Application("Windows 3.1") { BasePrice = Money.PLN(100) },
                            new Application("Widow 95") { BasePrice = Money.PLN(200) },
                            new Application("Windows 2000") { BasePrice = Money.PLN(500) }
                        });
                ctx.SaveChanges();
            }

            int changedRecords;
            using (var ctx = CreateDbContext(_output))
            {
                // Act
                var modelToFix = ctx.Applications.First(x => x.Name == "Widow 95");
                modelToFix.Name = "Windows 95";
                ctx.SaveChanges();

                IApplicationRepository repo = new ApplicationRepository(ctx);
                changedRecords = repo.ChangeCurrency("PLN", "EUR", 0.25m);

                // Assert
                Assert.Equal(3, changedRecords);


                // Happy to return results to the client
                var applicationsInEuro =
                    ctx.Applications.Where(x => x.BasePrice.Currency == "EUR").ToList();
                applicationsInEuro.ForEach(a => Assert.Equal("EUR", a.BasePrice.Currency));

                Assert.True(applicationsInEuro.Any(a => a.Name == "Windows 95"));
            }
        }

        private class Magician : IApplicationRepository
        {
            private readonly ApplicationRepository _repository;

            public static IApplicationRepository PleaseFixThis(IApplicationRepository repository)
            {
                return new Magician((ApplicationRepository)repository);
            }

            private Magician(ApplicationRepository repository)
            {
                _repository = repository;
            }

            public int ChangeCurrency(string fromCurrencyCode, string toCurrencyCode, decimal conversionRate)
            {
                var changedRecords = _repository.ChangeCurrency(
                    fromCurrencyCode,
                    toCurrencyCode,
                    conversionRate);
                foreach (var entry in _repository._dbContext.ChangeTracker.Entries<Application>().Where(x => x.Entity.BasePrice.Currency == fromCurrencyCode))
                {
                    if (entry.State == EntityState.Unchanged)
                    {
                        entry.State = EntityState.Detached;
                    }
                }
                return changedRecords;
            }
        }
    }
}