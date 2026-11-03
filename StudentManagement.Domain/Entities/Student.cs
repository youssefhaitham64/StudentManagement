using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.Domain.Entities;

public class Student
{
    public string StudentId { get; set; } = string.Empty;
    public float GPA { get; set; }
    public string School { get; set; } = string.Empty;

    public int UserId { get; set; }          // foreign key column
    public User User { get; set; } = null!;  // navigation property
}
