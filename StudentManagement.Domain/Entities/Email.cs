using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagement.Domain.Entities;

public class Email
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = string.Empty;

    public int UserId { get; set; }          // foreign key column
    public User User { get; set; } = null!;  // navigation property
}
