using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Education
{
    public int Id { get; set; }

    public string EducationPlace { get; set; } = null!;

    public DateTime EndDate { get; set; }

    public string Title { get; set; } = null!;

    public bool IsRemoved { get; set; }
}
