using System.Collections.Generic;
using System.Linq;

using Four2n.JustApp.DataAccess.EF;
using Four2n.JustApp.Domain.Applications;

using Xunit;
using Xunit.Abstractions;

namespace Four2n.JustApp.DataAccess.IntegrationTests.Examples
{
    public class BatchInsertingTests : RollbackFixture
    {
        private readonly ITestOutputHelper output;

        public BatchInsertingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class ApplicationRepository
        {
            private DomainDbContext dbContext;

            public ApplicationRepository(DomainDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public long Add(Application app)
            {
                dbContext.Applications.Add(app);
                dbContext.SaveChanges();
                return app.Id;
            }
        }

        [Fact]
        public void Add_to_repository_returns_generated_id()
        {
            // Arrange
            var repo = new ApplicationRepository(CreateDbContext());
            var app = new Application("DOS 5.1");

            // Act
            var result = repo.Add(app);

            // Assert
            Assert.NotEqual(0, result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Add_by_repository(int batchSize)
        {
            var ctx = CreateDbContext();
            var repo = new ApplicationRepository(ctx);

            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                foreach (var application in modelsToAdd)
                {
                    repo.Add(application);
                }
            }
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        //[InlineData(5000)]
        public void Add_one_by_one(int batchSize)
        {
            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                foreach (var application in modelsToAdd)
                {
                    ctx.Applications.Add(application);
                }

                ctx.SaveChanges();
            }
        }


        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Add_range(int batchSize)
        {
            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                ctx.Applications.AddRange(modelsToAdd);
                ctx.SaveChanges();
            }
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Add_one_by_one_without_change_tracking(int batchSize)
        {
            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                ctx.Configuration.AutoDetectChangesEnabled = false;
                foreach (var application in modelsToAdd)
                {
                    ctx.Applications.Add(application);
                }

                ctx.SaveChanges();
                ctx.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Add_range_without_change_tracking(int batchSize)
        {
            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                ctx.Configuration.AutoDetectChangesEnabled = false;
                ctx.Applications.AddRange(modelsToAdd);
                ctx.SaveChanges();
                ctx.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Add_range_without_change_tracking_and_validation(int batchSize)
        {
            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(batchSize, ctx);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                ctx.Configuration.AutoDetectChangesEnabled = false;
                ctx.Configuration.ValidateOnSaveEnabled = false;
                ctx.Applications.AddRange(modelsToAdd);
                ctx.SaveChanges();
                ctx.Configuration.ValidateOnSaveEnabled = true;
                ctx.Configuration.AutoDetectChangesEnabled = true;
                output.WriteLine(ctx.ChangeTracker.Entries().Count().ToString());
            }
        }

        private List<Application> CreateModelsToAdd(int count, DomainDbContext ctx)
        {
            var europe = ctx.OrganizationalUnits.First(o => o.Name == "Europe");

            var modelsToAdd = new List<Application>();

            for (int i = 0; i < count; i++)
            {
                var application = new Application("Doom" + i);
                application.RentedTo.Add(new ApplicationRental(application, europe, true));
                modelsToAdd.Add(application);
            }
            return modelsToAdd;
        }
    }
}