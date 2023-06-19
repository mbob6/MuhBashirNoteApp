using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Entities;

namespace NoteApp.Context
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");
            builder.HasKey(q => q.Id);

            builder.HasOne(q => q.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(q => q.UserId)
                .IsRequired();

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(q => q.Content)
                .IsRequired()
                .HasMaxLength(999999999);
        }
    }
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(r => r.RoleName)
                     .IsUnique();

            builder.Property(r => r.Description)
                   .HasMaxLength(200);

            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId);
        }
    }
    public class UserEntityTypeConfig : IEntityTypeConfiguration<User>
	{
        public void Configure(EntityTypeBuilder<User> builder)
        {
			builder.ToTable("Users");

			builder.HasKey(u => u.Id);

			builder.Property(u => u.UserName)
				.IsRequired()
				.HasMaxLength(10);

			builder.Property(u => u.Email)
				.IsRequired();

			builder.Property(u => u.RoleId)
				.IsRequired();

			builder.HasOne(u => u.Role)
				.WithMany(r => r.Users)
				.HasForeignKey(u => u.RoleId);
		}
    }
}
