using System;
using System.Collections.Generic;

namespace MovieApp.Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Gender { get; set; }

    public int UserTypeId { get; set; }

    public virtual UserType UserType { get; set; } = null!;
}
