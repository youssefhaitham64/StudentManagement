using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    // Navigation properties (not columns in the table)
    public Student? Student { get; set; }
    public ICollection<Email> Emails { get; set; } = new List<Email>();
}
