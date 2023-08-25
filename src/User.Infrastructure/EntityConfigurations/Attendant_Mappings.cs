using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.AttendantAgregattes;

namespace User.Infrastructure.EntityConfigurations;

internal class Attendant_Mappings : IEntityTypeConfiguration<Attendant>
{
    public void Configure(EntityTypeBuilder<Attendant> builder)
    {
        builder.HasKey(t => t.Id);
        builder.ToTable("Attendant");

        builder.Property(t => t.Id).HasColumnName("ID").HasColumnType("uuid");
        builder.Property(t => t.IdUser).HasColumnName("ID_USER").HasColumnType("uuid");

        builder.Property(t => t.Name).HasColumnName("NAME");
        builder.HasOne(tc => tc.UserAcess).WithOne().HasForeignKey<Attendant>(f => f.IdUser);
    }
}

