using System;

namespace SellAutoApp.Models;

public partial class CarModelPrice
{
    public int CarModelPriceId { get; set; }

    public float CarModelPrice1 { get; set; }

    public DateTime CarModelPriceUpdate { get; set; }

    public int CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;
}
