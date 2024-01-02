using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Skill
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Percent { get; set; } = null!;

    public bool IsRemoved { get; set; }
}
