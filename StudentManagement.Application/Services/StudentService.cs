using System;
using System.Collections.Generic;
using System.Text;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    // The repository is "injected" by the DI container (see Part 8)
    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();

        return students.Select(s => new StudentDto
        {
            StudentId = s.StudentId,
            FullName = s.User.FullName,
            GPA = s.GPA,
            School = s.School
        }).ToList();
    }

    public async Task<StudentDetailsDto?> GetByIdAsync(string studentId)
    {
        var student = await _repository.GetByIdAsync(studentId);
        return student is null ? null : MapToDetails(student);
    }

    public async Task<StudentDetailsDto?> CreateAsync(CreateStudentDto dto)
    {
        // A StudentId is a primary key, so it must be unique
        if (await _repository.ExistsAsync(dto.StudentId))
            return null;

        var student = new Student
        {
            StudentId = dto.StudentId,
            GPA = dto.GPA,
            School = dto.School,
            User = new User
            {
                FullName = dto.FullName,
                Emails = dto.Emails
                    .Select(address => new Email { EmailAddress = address })
                    .ToList()
            }
        };

        // EF Core inserts the User, then the Student and the Emails, in one transaction
        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();

        return MapToDetails(student);
    }

    public async Task<bool> UpdateAsync(string studentId, UpdateStudentDto dto)
    {
        var student = await _repository.GetByIdAsync(studentId);
        if (student is null)
            return false;

        student.GPA = dto.GPA;
        student.School = dto.School;
        student.User.FullName = dto.FullName;   // FullName lives in the Users table

        await _repository.SaveChangesAsync();   // EF Core detects the changes and runs UPDATE
        return true;
    }

    public async Task<bool> DeleteAsync(string studentId)
    {
        var student = await _repository.GetByIdAsync(studentId);
        if (student is null)
            return false;

        _repository.Delete(student);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static StudentDetailsDto MapToDetails(Student s) => new()
    {
        StudentId = s.StudentId,
        UserId = s.UserId,
        FullName = s.User.FullName,
        GPA = s.GPA,
        School = s.School,
        Emails = s.User.Emails.Select(e => e.EmailAddress).ToList()
    };
}
