using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.AttendantAgregattes;
using User.Domain.AgregattesModel.ClientAgregattes;

namespace User.Infrastructure.EntityConfigurations;

internal class Attendant_Mapping : IEntityTypeConfiguration<Attendant>
{
    public void Configure(EntityTypeBuilder<Attendant> builder)
    {
        builder.ToTable("Attendant");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("id").HasColumnType("uuid");
        builder.Property(t => t.IdUser).HasColumnName("id_user").HasColumnType("uuid");

        builder.Property(t => t.Name).HasColumnName("name");
        builder.HasOne(tc => tc.UserAcess).WithOne().HasForeignKey<Attendant>(f => f.IdUser);
    }
}
