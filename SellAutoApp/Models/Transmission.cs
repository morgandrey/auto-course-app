using System.Collections.Generic;

namespace SellAutoApp.Models;

public partial class Transmission
{
    public int TransmissionId { get; set; }

    public string TransmissionName { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; } = new List<CarModel>();
}
