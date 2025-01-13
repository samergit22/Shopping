using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Models;

namespace HirePlatform.Persistence
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

        }
    }
}
