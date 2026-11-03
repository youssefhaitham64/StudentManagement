using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(100);

        // Mock data
        builder.HasData(
            new User { Id = 1, FullName = "Ahmed Mostafa" },
            new User { Id = 2, FullName = "Mariam Adel" },
            new User { Id = 3, FullName = "Omar Khaled" },
            new User { Id = 4, FullName = "Nour Samir" },
            new User { Id = 5, FullName = "Salma Hany" });
    }
}