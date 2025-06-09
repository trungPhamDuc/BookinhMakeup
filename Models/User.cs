using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ArtistMedia> ArtistMedia { get; set; } = new List<ArtistMedia>();

    public virtual ICollection<Booking> BookingArtists { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingCustomers { get; set; } = new List<Booking>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<MakeupArtist> MakeupArtists { get; set; } = new List<MakeupArtist>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<UserBankAccount> UserBankAccounts { get; set; } = new List<UserBankAccount>();
}
