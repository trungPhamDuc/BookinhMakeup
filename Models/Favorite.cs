using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public Guid MakeupArtistId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MakeupArtist MakeupArtist { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
