using System;
using System.Collections.Generic;
using System.Text;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(string studentId);
    Task<bool> ExistsAsync(string studentId);
    Task AddAsync(Student student);
    void Delete(Student student);
    Task SaveChangesAsync();
}