using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpMan.Domain.Entities;

namespace RpMan.Persistence.Configurations
{
    public class UserRoleGroupConfiguration : IEntityTypeConfiguration<UserRoleGroup>
    {
        public void Configure(EntityTypeBuilder<UserRoleGroup> builder)
        {
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false);

            // builder.HasAlternateKey(e => e.Name).HasName("AlternativeKey_Name"); // replace by below as this line doesnt allow change sof fields easily
            builder.HasIndex(x => x.Name).IsUnique();

        }
    }
}
