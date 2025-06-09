using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Membership
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string? UserType { get; set; }

    public string? Benefits { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid UserId { get; set; }

    public DateOnly? LastAccessDate { get; set; }

    public virtual User User { get; set; } = null!;
}
