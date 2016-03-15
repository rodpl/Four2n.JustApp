using System.Collections.Generic;

using Four2n.JustApp.Domain.Applications;

using Xunit;
using Xunit.Abstractions;

namespace Four2n.DataAccess.IntegrationTests.Examples
{
    public class BatchInsertingTests : RollbackFixture
    {
        private readonly ITestOutputHelper output;

        public BatchInsertingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void FactMethodName()
        {
            const int BatchSize = 1000;

            var ctx = CreateDbContext();
            var modelsToAdd = CreateModelsToAdd(BatchSize);

            using (Benchmark.InMiliseconds().ToOutput(output, "Just Add: {0}"))
            {
                foreach (var application in modelsToAdd)
                {
                    ctx.Applications.Add(application);
                }

                ctx.SaveChanges();
            }
        }

        private static List<Application> CreateModelsToAdd(int count)
        {
            var modelsToAdd = new List<Application>();

            for (int i = 0; i < count; i++)
            {
                modelsToAdd.Add(new Application("Doom" + i));
            }
            return modelsToAdd;
        }
    }
}