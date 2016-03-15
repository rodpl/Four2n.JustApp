using System;

using Four2n.JustApp.DataAccess.EF;

namespace Four2n.JustApp.DataAccess.IntegrationTests
{
    public class DatabaseBootstrapFixture : IDisposable
    {
        internal const string DomainConnectionString = "Server=(local);Integrated Security=True;Database=JustApp_IntegrationTests;";

        public DatabaseBootstrapFixture()
        {
            DomainDbContext.Initialize(DomainConnectionString);
        }

        public void Dispose()
        {
        }
    }
}