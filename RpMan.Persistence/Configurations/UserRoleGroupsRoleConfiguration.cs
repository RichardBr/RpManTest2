using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpMan.Domain.Entities;

namespace RpMan.Persistence.Configurations
{
    public class UserRoleGroupsRoleConfiguration : IEntityTypeConfiguration<UserRoleGroupsRole>
    {
        public void Configure(EntityTypeBuilder<UserRoleGroupsRole> builder)
        {
            builder.HasKey(ur => new { ur.UserRoleGroupId, ur.RoleId });

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoleGroupsRoles)
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired();

            builder.HasOne(ur => ur.UserRoleGroup)
                   .WithMany(r => r.UserRoleGroupsRoles)
                   .HasForeignKey(ur => ur.UserRoleGroupId)
                   .IsRequired();
        }
    }
}
