using System;
using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; } = new List<CarModel>();
}
