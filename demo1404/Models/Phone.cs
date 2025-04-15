using System;
using System.Collections.Generic;

namespace demo1404.Models;

public partial class Phone
{
    public int Id { get; set; }

    public string Model { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public int? CompanyId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual Company? Company { get; set; }
}
