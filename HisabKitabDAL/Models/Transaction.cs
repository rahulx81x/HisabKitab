using System;
using System.Collections.Generic;

namespace HisabKitabDAL.Models;

public partial class Transaction
{
    public int UserId { get; set; }

    public int TranId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string Type { get; set; } = null!;

    public string? Remarks { get; set; }

    public virtual User User { get; set; } = null!;
}
