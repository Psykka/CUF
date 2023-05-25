using System.ComponentModel.DataAnnotations.Schema;

namespace CUF.Models;

[Table("OrderInformation")]
public class OrderInformationModel
{
    public int Id { get; set; }

    public OrderModel? Order { get; set; }

    public ProductModel? Product { get; set; }

    public SupplierModel? Supplier { get; set; }

    public int Quantity { get; set; }
}