using System.Data.Entity.ModelConfiguration;

using Four2n.JustApp.Domain.SharedKernel;

namespace Four2n.JustApp.DataAccess.EF.Mappings
{
    public class MoneyMap : ComplexTypeConfiguration<Money>
    {
        public MoneyMap()
        {
        }
    }
}