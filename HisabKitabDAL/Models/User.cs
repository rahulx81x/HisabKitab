using System;
using System.Collections.Generic;

namespace HisabKitabDAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
