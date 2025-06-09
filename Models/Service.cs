using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Service
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Duration { get; set; }

    public decimal BasePrice { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MakeupArtist> Artists { get; set; } = new List<MakeupArtist>();
}
