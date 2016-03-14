using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Four2n.JustApp.DataAccess.EF;
using Four2n.JustApp.Domain.Organizational;

using Xunit;

namespace Four2n.DataAccess.IntegrationTests
{
    public class SimpleTests : RollbackFixture
    {

        public SimpleTests()
        {
        }

        [Fact]
        public void PassingTest()
        {
            using (var ctx = this.CreateDbContext())
            {
                ctx.OrganizationalUnits.Add(new OrganizationalUnit { Name = "World" });
                ctx.SaveChanges();
            }
        }

        [Fact]
        public void Ansther()
        {
        }
    }
}
