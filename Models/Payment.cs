using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? PaidAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
