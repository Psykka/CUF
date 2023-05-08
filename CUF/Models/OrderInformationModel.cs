using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUF.Models;

[Table("OrderInformation")]
public class OrderInformationModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public OrderModel? Order { get; set; }

    public ProductModel? Product { get; set; }

    public int Quantity { get; set; }
}