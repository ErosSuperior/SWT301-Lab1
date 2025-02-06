using System;
using System.Collections.Generic;

namespace ecomerce.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool Type { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
