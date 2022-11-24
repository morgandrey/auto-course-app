using System;
using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string UserRoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
