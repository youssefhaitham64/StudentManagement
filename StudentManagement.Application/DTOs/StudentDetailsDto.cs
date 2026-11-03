using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.Application.DTOs;

public class StudentDetailsDto
{
    public string StudentId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public float GPA { get; set; }
    public string School { get; set; } = string.Empty;
    public List<string> Emails { get; set; } = new();
}