using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs;

public class CreateStudentDto
{
    [Required, StringLength(20)]
    public string StudentId { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Range(0.0, 4.0)]
    public float GPA { get; set; }

    [Required, StringLength(100)]
    public string School { get; set; } = string.Empty;

    public List<string> Emails { get; set; } = new();
}