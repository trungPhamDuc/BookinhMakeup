using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class History
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid BookingId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
