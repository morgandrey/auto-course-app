using System;
using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class Color
{
    public int ColorId { get; set; }

    public string ColorName { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; } = new List<CarModel>();
}
