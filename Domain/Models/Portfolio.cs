using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Portfolio
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageTitle { get; set; } 

    public string Link { get; set; } = null!;

    public string ImageAlt { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int ViewCount { get; set; } = 0;

    public bool IsRemoved { get; set; } = false;
    public string? Video { get; set; }

}
