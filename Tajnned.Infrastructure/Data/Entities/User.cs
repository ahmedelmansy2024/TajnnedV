using System;
using System.Collections.Generic;

namespace Tajnned.Infrastructure.Data.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
