using System.Data.Entity.Core.Mapping;
using System.Data.Entity.ModelConfiguration;

using Four2n.JustApp.Domain.Applications;

namespace Four2n.JustApp.DataAccess.EF.Mappings
{
    public class ApplicationMap : EntityTypeConfiguration<Application>
    {
        public ApplicationMap()
        {
            HasMany(m => m.RentedTo);
        }
    }
}