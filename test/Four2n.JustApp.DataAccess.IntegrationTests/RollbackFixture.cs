﻿using System;
using System.Transactions;

using Four2n.JustApp.DataAccess.EF;

using Xunit;
using Xunit.Abstractions;

namespace Four2n.JustApp.DataAccess.IntegrationTests
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
        protected DomainDbContext CreateDbContext(ITestOutputHelper logTo)
        {
            var context = new DomainDbContext(DatabaseBootstrapFixture.DomainConnectionString);
            context.Database.Log = s => logTo.WriteLine("SQL: {0}", s);
            return context;
        }
    }
}