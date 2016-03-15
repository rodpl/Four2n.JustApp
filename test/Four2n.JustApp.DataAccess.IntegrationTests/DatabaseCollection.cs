using Xunit;

namespace Four2n.JustApp.DataAccess.IntegrationTests
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseBootstrapFixture>
    {
    }
}