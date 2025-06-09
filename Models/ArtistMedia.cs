using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class ArtistMedia
{
    public Guid Id { get; set; }

    public Guid ArtistId { get; set; }

    public string MediaUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public bool? IsPublic { get; set; }

    public virtual User Artist { get; set; } = null!;
}
