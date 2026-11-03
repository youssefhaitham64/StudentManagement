using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students
            .AsNoTracking()               // read-only: faster, no change tracking needed
            .Include(s => s.User)         // JOIN Users to get FullName
            .OrderBy(s => s.StudentId)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(string studentId)
    {
        return await _context.Students
            .Include(s => s.User)
                .ThenInclude(u => u.Emails)   // JOIN Emails too: "full information"
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public Task<bool> ExistsAsync(string studentId)
    {
        return _context.Students.AnyAsync(s => s.StudentId == studentId);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public void Delete(Student student)
    {
        // Deleting the User removes the person completely:
        // cascade delete also removes the Student row and all their Emails
        _context.Users.Remove(student.User);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}