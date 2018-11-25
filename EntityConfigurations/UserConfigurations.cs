using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi_basic_mysql.Models;

namespace webapi_basic_mysql.EntityConfigurations
{
public class UserConfigurations : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Name
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Configure columns
            builder.Property(u => u.Email).HasColumnName("Email").IsRequired(true);
            builder.HasIndex(u => u.Email).IsUnique();
        }

    }
}