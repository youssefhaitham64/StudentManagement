using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.Application.DTOs;

public class StudentDto
{
    public string StudentId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public float GPA { get; set; }
    public string School { get; set; } = string.Empty;
}
