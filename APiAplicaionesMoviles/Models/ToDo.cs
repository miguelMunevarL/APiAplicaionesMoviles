using System;
using System.Collections.Generic;

namespace APiAplicaionesMoviles.Models;

public partial class ToDo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Status { get; set; }
}
