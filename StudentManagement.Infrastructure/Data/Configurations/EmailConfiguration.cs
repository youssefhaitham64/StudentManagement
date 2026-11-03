using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable("Emails");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmailAddress)
               .IsRequired()
               .HasMaxLength(256);

        // One User has many Emails (one-to-many)
        builder.HasOne(e => e.User)
               .WithMany(u => u.Emails)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Email { Id = 1, EmailAddress = "ahmed.mostafa@example.com", UserId = 1 },
            new Email { Id = 2, EmailAddress = "ahmed.personal@example.com", UserId = 1 },
            new Email { Id = 3, EmailAddress = "mariam.adel@example.com", UserId = 2 },
            new Email { Id = 4, EmailAddress = "omar.khaled@example.com", UserId = 3 },
            new Email { Id = 5, EmailAddress = "nour.samir@example.com", UserId = 4 },
            new Email { Id = 6, EmailAddress = "nour.work@example.com", UserId = 4 },
            new Email { Id = 7, EmailAddress = "salma.hany@example.com", UserId = 5 });
    }
}