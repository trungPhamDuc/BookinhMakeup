using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Review
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
