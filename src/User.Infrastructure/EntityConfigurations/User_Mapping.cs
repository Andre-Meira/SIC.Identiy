using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AgregattesModel.UserAgregattes;
using User.Domain.AgregattesModel.ValueObjects;

namespace User.Infrastructure.EntityConfigurations;

internal class UserAcess_Mapping : IEntityTypeConfiguration<UserAcess>
{
    public void Configure(EntityTypeBuilder<UserAcess> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("UserAcess");        
    }
}
