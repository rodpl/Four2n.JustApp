using Xunit;

namespace Four2n.DataAccess.IntegrationTests
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseBootstrapFixture>
    {
    }
}