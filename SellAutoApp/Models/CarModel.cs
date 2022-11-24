using System;
using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class CarModel
{
    public int CarModelId { get; set; }

    public string CarModelName { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int ColorId { get; set; }

    public int TransmissionId { get; set; }

    public virtual ICollection<CarModelOrder> CarModelOrders { get; } = new List<CarModelOrder>();

    public virtual ICollection<CarModelPrice> CarModelPrices { get; } = new List<CarModelPrice>();

    public virtual Color Color { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Transmission Transmission { get; set; } = null!;
}
