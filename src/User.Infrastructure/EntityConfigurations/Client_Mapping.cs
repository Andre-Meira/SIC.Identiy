using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.UserAgregattes;

namespace User.Infrastructure.EntityConfigurations;

internal class Client_Mapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");        
        builder.Property(e => e.AcceptedNotification).HasColumnName("IS_NOTIFY").IsRequired(true);
    }
}
