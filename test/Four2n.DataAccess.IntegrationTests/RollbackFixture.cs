using System;
using System.Transactions;

using Four2n.JustApp.DataAccess.EF;

using Xunit;

namespace Four2n.DataAccess.IntegrationTests
{
    [Collection("Database collection")]
    public abstract class RollbackFixture : IDisposable
    {
        private readonly TransactionScope _transaction;

        protected RollbackFixture()
        {
            _transaction = new TransactionScope();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        protected DomainDbContext CreateDbContext()
        {
            return new DomainDbContext(DatabaseBootstrapFixture.DomainConnectionString);
        }
    }
}