using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using Four2n.JustApp.Domain.Applications;

namespace Four2n.JustApp.DataAccess.EF.Mappings
{
    public class ApplicationMap : EntityTypeConfiguration<Application>
    {
        private const string NameAlternateKeyNameDefinition = "AK_dbo.Applications_Name";

        public ApplicationMap()
        {
/*            Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute(NameAlternateKeyNameDefinition) { IsUnique = true }));*/
            HasMany(m => m.RentedTo);
            Property(m => m.BasePrice.Amount);
            Property(m => m.BasePrice.Currency);
        }
    }
}