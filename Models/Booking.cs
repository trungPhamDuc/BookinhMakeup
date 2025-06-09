using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ArtistId { get; set; }

    public Guid ServiceId { get; set; }

    public DateTime BookingTime { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Artist { get; set; } = null!;

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Service Service { get; set; } = null!;
}
