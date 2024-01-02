using System;
using System.Collections.Generic;

namespace Domain.Models;

public class Pricing
{
    public int Id { get; set; }

    public string Amount { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public bool IsRemoved { get; set; } = false;
}
