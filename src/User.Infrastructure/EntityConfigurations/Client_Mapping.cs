using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.ClientAgregattes;

namespace User.Infrastructure.EntityConfigurations;

internal class Client_Mapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID").HasColumnType("uuid");
        builder.Property(t => t.IdUser).HasColumnName("ID_USER").HasColumnType("uuid");

        builder.Property(t => t.Name).HasColumnName("NAME");
        builder.HasOne(tc => tc.UserAcess).WithOne().HasForeignKey<Client>(f => f.IdUser);
    }
}
