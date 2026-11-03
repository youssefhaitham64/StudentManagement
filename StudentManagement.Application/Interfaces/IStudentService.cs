using System;
using System.Collections.Generic;
using System.Text;
using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<StudentDetailsDto?> GetByIdAsync(string studentId);
    Task<StudentDetailsDto?> CreateAsync(CreateStudentDto dto);
    Task<bool> UpdateAsync(string studentId, UpdateStudentDto dto);
    Task<bool> DeleteAsync(string studentId);
}