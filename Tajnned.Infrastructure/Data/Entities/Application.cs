using System;
using System.Collections.Generic;

namespace Tajnned.Infrastructure.Data.Entities;

public partial class Application
{
    public Guid Id { get; set; }

    public string? ApplicationNumber { get; set; }

    public bool IsDeleted { get; set; }
}
