using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserHash { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public int UserRoleId { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual UserRole UserRole { get; set; } = null!;
}
