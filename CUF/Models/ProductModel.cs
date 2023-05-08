using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUF.Models;

public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Unit { get; set; }

    public ICollection<SupplierModel>? Suppliers { get; set; }

    public ICollection<AttachmentModel>? Attachment { get; set; }
}