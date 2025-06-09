using System;
using System.Collections.Generic;

namespace www_Blush_Brush.Models;

public partial class UserBankAccount
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string BankName { get; set; } = null!;

    public string AccountHolderName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string? RoutingNumber { get; set; }

    public string? Iban { get; set; }

    public string? SwiftCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
