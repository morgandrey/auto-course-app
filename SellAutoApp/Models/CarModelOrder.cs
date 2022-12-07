namespace SellAutoApp.Models;

public partial class CarModelOrder
{
    public int Id { get; set; }

    public int CarModelId { get; set; }

    public int OrderId { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
