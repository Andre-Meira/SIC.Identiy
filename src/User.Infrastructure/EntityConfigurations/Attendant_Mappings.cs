using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Infrastructure.EntityConfigurations;

internal class Attendant_Mappings : IEntityTypeConfiguration<Attendant>
{
    public void Configure(EntityTypeBuilder<Attendant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Attendant");

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.DtCreation).HasColumnName("DT_CREATED").IsRequired();
        builder.Property(e => e.Status).HasColumnName("STATUS").IsRequired();

        builder.Property(e => e.Email).HasColumnName("EMAIL").HasConversion
            (valueEmail => valueEmail.Value,
             Value => new Email(Value))
            .IsUnicode(true)
            .IsRequired();
    }
}

