using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUF.Models;

public enum ProductType {
    Product = 1,
    Service
}

public enum ProductUnit {
    Unit = 1,
    Kg,
    L,
    M,
    M2,
    M3,
    Box,
    Pallet,
    Package,
    Other
}

public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    public string? Unit { get; set; }

    [Required]
    public ProductType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<SupplierModel>? Suppliers { get; set; }

    public ICollection<AttachmentModel>? Attachment { get; set; }
}