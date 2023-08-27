using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.UserAgregattes;

namespace User.Infrastructure.EntityConfigurations;

internal class UserAcess_Mapping : IEntityTypeConfiguration<UserAcess>
{
    public void Configure(EntityTypeBuilder<UserAcess> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("User_acess");        

        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid");
        builder.Property(e => e.Name).HasColumnName("name").IsRequired();
        builder.Property(e => e.DtCreation).HasColumnName("dt_created").IsRequired();
        builder.Property(e => e.Status).HasColumnName("status").IsRequired();
        
        builder.OwnsOne(e => e.Email, a => a.Property(e => e.Value).HasColumnName("email"));
        builder.OwnsOne(e => e.Password, a => a.Property(e => e.Value).HasColumnName("password"));        
    }
}
