using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Icon { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Count { get; set; } = null!;

    public bool IsRemoved { get; set; }
}
