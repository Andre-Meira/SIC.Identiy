using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Infrastructure.EntityConfigurations;

internal class User_Mapping : IEntityTypeConfiguration<UserAcess>
{
    public void Configure(EntityTypeBuilder<UserAcess> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("UserAcess");        

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.DtCreation).HasColumnName("DT_CREATED").IsRequired();
        builder.Property(e => e.Status).HasColumnName("STATUS").IsRequired();

        builder.Property(e => e.Email).HasColumnName("EMAIL").HasConversion
            (valueEmail => valueEmail.Value,
             Value => new Email(Value))
            .IsUnicode(true)
            .IsRequired();

        builder.Property(e => e.Password).HasColumnName("PASSWORD").HasConversion
            (valuePassword => valuePassword.Value,
             Value => new Password(Value))
            .IsRequired();
    }
}
