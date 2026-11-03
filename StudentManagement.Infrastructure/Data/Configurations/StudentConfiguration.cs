using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        // StudentId is a string, so EF Core can't guess it's the key by name: we say it explicitly
        builder.HasKey(s => s.StudentId);
        builder.Property(s => s.StudentId).HasMaxLength(20);

        builder.Property(s => s.School)
               .IsRequired()
               .HasMaxLength(100);

        // One User is one Student (one-to-one)
        builder.HasOne(s => s.User)
               .WithOne(u => u.Student)
               .HasForeignKey<Student>(s => s.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Student { StudentId = "202100101", GPA = 3.45f, School = "Engineering", UserId = 1 },
            new Student { StudentId = "202100102", GPA = 3.82f, School = "Computational Sciences and AI", UserId = 2 },
            new Student { StudentId = "202100103", GPA = 2.95f, School = "Science", UserId = 3 },
            new Student { StudentId = "202100104", GPA = 3.60f, School = "Business", UserId = 4 },
            new Student { StudentId = "202100105", GPA = 3.10f, School = "Engineering", UserId = 5 });
    }
}