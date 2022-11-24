using System;
using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<CarModelOrder> CarModelOrders { get; } = new List<CarModelOrder>();

    public virtual User User { get; set; } = null!;
}
