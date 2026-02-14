using App.Features.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping;

public sealed class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(u => u.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(255)")
            .IsRequired();

        builder.HasIndex(u => u.Email, "Unique_Key_Users_Email")
            .IsUnique();

        builder.Property(u => u.Phone)
            .HasColumnName("Phone")
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        builder.HasIndex(u => u.Phone, "Unique_Key_Users_Phone")
            .IsUnique();

        builder.Property(u => u.Password)
            .HasColumnName("Password")
            .HasColumnType("nvarchar(255)")
            .IsRequired();

        builder.Property(u => u.Created)
            .HasColumnName("Created")
            .HasColumnType("datetime2")
            .IsRequired();
    }
}