using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace www_Blush_Brush.Models;

public partial class BookingMakeupContext : DbContext
{
    public BookingMakeupContext()
    {
    }

    public BookingMakeupContext(DbContextOptions<BookingMakeupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArtistMedia> ArtistMedia { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<MakeupArtist> MakeupArtists { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBankAccount> UserBankAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(local);database=Booking_Makeup;uid=sa;pwd=duc123;Encrypt=false;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtistMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__artist_m__3213E83FA87B4D4D");

            entity.ToTable("artist_media");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(true)
                .HasColumnName("is_public");
            entity.Property(e => e.MediaUrl)
                .IsUnicode(false)
                .HasColumnName("media_url");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("uploaded_at");

            entity.HasOne(d => d.Artist).WithMany(p => p.ArtistMedia)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__artist_me__artis__6D0D32F4");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F648EC885");

            entity.ToTable("bookings");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.BookingTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_time");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Artist).WithMany(p => p.BookingArtists)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__artist__5441852A");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__bookings__custom__534D60F1");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__servic__5535A963");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__favorite__3213E83F5F043BBB");

            entity.ToTable("favorites");

            entity.HasIndex(e => new { e.UserId, e.MakeupArtistId }, "UQ_Favorite").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MakeupArtistId).HasColumnName("makeup_artist_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.MakeupArtist).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.MakeupArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_MakeupArtist");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Favorites_User");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__history__3213E83F358D9E79");

            entity.ToTable("history");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.Histories)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__history__booking__73BA3083");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__history__user_id__72C60C4A");
        });

        modelBuilder.Entity<MakeupArtist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__makeup_a__3213E83F5E0ABD5C");

            entity.ToTable("makeup_artists");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.RatingAvg).HasColumnName("rating_avg");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.MakeupArtists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__makeup_ar__user___45F365D3");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__membersh__3213E83F93447886");

            entity.ToTable("memberships");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Benefits).HasColumnName("benefits");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_type");

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__membershi__user___412EB0B6");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payments__3213E83F56AD9ECA");

            entity.ToTable("payments");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PaidAt)
                .HasColumnType("datetime")
                .HasColumnName("paid_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__payments__bookin__619B8048");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83FD588D186");

            entity.ToTable("reviews");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Booking).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__reviews__booking__6754599E");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83F236229C3");

            entity.ToTable("services");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base_price");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasMany(d => d.Artists).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceMakeupArtist",
                    r => r.HasOne<MakeupArtist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__service_m__artis__4D94879B"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__service_m__servi__4CA06362"),
                    j =>
                    {
                        j.HasKey("ServiceId", "ArtistId").HasName("PK__service___38C0BCAFDB8BFEDD");
                        j.ToTable("service_makeup_artist");
                        j.IndexerProperty<Guid>("ServiceId").HasColumnName("service_id");
                        j.IndexerProperty<Guid>("ArtistId").HasColumnName("artist_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F95FB3DC6");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164AEE39028").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .IsUnicode(false)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<UserBankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_ban__3213E83F9B5B1EF6");

            entity.ToTable("user_bank_accounts");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountHolderName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_holder_name");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_number");
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bank_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Iban)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("iban");
            entity.Property(e => e.RoutingNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("routing_number");
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("swift_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserBankAccounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_bank__user___5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
