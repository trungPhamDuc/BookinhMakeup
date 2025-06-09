using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class MakeupArtist
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Bio { get; set; }

    public int? Experience { get; set; }

    public double? RatingAvg { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
